using org.apache.zookeeper;
using org.apache.zookeeper.data;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZooBrowser.ViewModel;
using ZookeeperBrowser.ViewModel;

namespace ZookeeperBrowser.Services
{
    public interface IZookeeperService
    {
        string CnnString { set; }

        Task<IEnumerable<NodeViewModel>> GetChildrenAsync(string path);

        Task<DataViewModel> GetDataAsync(string path);

        Task<IEnumerable<NodeViewModel>> GetAllAsync();

        Task<string> CreateAsync(string path, byte[] data, List<ACL> acl, CreateMode createMode);

        Task DeleteAsync(string path);

        /// <summary>
        /// 递归删除
        /// </summary>
        Task<bool> DeleteRecursiveAsync(string path);
    }
}