using org.apache.zookeeper;
using org.apache.zookeeper.common;
using org.apache.zookeeper.data;
using ZookeeperBrowser.Utils;
using ZookeeperBrowser.ViewModel;

namespace ZookeeperBrowser.Services
{
    public class ZookeeperService : IZookeeperService
    {
        private readonly IConfiguration _configuration;

        public ZookeeperService(IConfiguration configuration)
        {
            _configuration = configuration;

            //设置默认连接
            _cnnString = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList().FirstOrDefault() ??
                         "127.0.0.1";
        }

        private string _cnnString;

        public string CnnString
        {
            private get { return _cnnString; }
            set
            {
                _api = null;
                _cnnString = value;
            }
        }

        private ZooKeeper _api;

        //会话超时时间,单位毫秒
        private int sessionTimeOut = 10000;

        private ZooKeeper Api
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CnnString))
                {
                    throw new Exception("Zookeeper连接字符串未定义。请在app.config中至少添加一个.");
                }

                //异步监听
                var watcher = new MyWatcher("ConnectWatcher");

                //Zookeeper的连接状态有6种：　　
                // //ZooKeeper.States的枚举
                // CONNECTING = 0,　　//连接中
                // CONNECTED = 1,　　 //已连接
                // CONNECTEDREADONLY = 2,　　//已连接，但是只能只读访问
                // CLOSED = 3,　　　　//已关闭连接
                // AUTH_FAILED = 4,　　　　//认证失败
                // NOT_CONNECTED = 5　　//未连接

                if (_api == null || _api.getState() == ZooKeeper.States.NOT_CONNECTED)
                {
                    _api = new ZooKeeper(CnnString, sessionTimeOut, watcher, true);
                    Thread.Sleep(1000); //停一秒，等待连接完成
                }

                return _api;
            }
        }

        public async Task<IEnumerable<NodeViewModel>> GetChildrenAsync(string path)
        {
            ValidatePath(path);
            var nodes = await Api.getChildrenAsync(path);
            return nodes.Children.Select(x => new NodeViewModel(this, path, x));
        }

        public async Task<DataViewModel> GetDataAsync(string path)
        {
            ValidatePath(path);
            var data = await Api.getDataAsync(path);
            return new DataViewModel(data.Data, data.Stat);
        }

        public async Task<IEnumerable<NodeViewModel>> GetAllAsync()
        {
            var rootNodes = await Api.getChildrenAsync("/");
            var allTasks = rootNodes.Children.Select(x => ZKUtil.listSubTreeBFS(Api, "/" + x));
            var allNodes = (await Task.WhenAll(allTasks)).SelectMany(x => x).ToArray();
            return allNodes.Select(x => new NodeViewModel(this, x, GetNodeName(x))).ToArray();
        }

        private void ValidatePath(string path)
        {
            try
            {
                PathUtils.validatePath(path);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("节点路径 '{0}' 是无效的. {1}", path, ex.Message));
            }
        }

        private string GetNodeName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            var segments = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            return segments?.LastOrDefault();
        }

        public Task<string> CreateAsync(string path, byte[] data, List<ACL> acl, CreateMode createMode)
        {
            ValidatePath(path);
            return Task.FromResult("没写方法逻辑");
        }

        public async Task DeleteAsync(string path)
        {
            ValidatePath(path);
            await Api.deleteAsync(path);
        }

        public async Task<bool> DeleteRecursiveAsync(string path)
        {
            ValidatePath(path);
            List<string> children;
            try
            {
                children = (await Api.getChildrenAsync(path)).Children;
            }
            catch (KeeperException.NoNodeException)
            {
                return true;
            }

            foreach (var subPath in children)
            {
                if (!await this.DeleteRecursiveAsync(path + "/" + subPath))
                {
                    return false;
                }
            }

            await this.DeleteAsync(path);
            return true;
        }
    }
}