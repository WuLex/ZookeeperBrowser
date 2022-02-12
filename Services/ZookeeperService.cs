using org.apache.zookeeper;
using org.apache.zookeeper.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooBrowser.ViewModel;
using ZookeeperBrowser.ViewModel;

namespace ZookeeperBrowser.Services
{
    public class ZookeeperService : IZookeeperService
    {
        private string _cnnString;
        
        public string CnnString
        {
            private get
            {
                return _cnnString;
            }
            set
            {
                _api = null;
                _cnnString = value;
            }
        }

        private ZooKeeper _api;

        private ZooKeeper Api
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CnnString))
                {
                    throw new Exception("Zookeeper连接字符串未定义。请在app.config中至少添加一个.");
                }

                if (_api == null || _api.getState() == ZooKeeper.States.NOT_CONNECTED)
                {
                    _api = new ZooKeeper(CnnString, 5000, null, true);
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

            return segments.LastOrDefault();
        }
    }
}
