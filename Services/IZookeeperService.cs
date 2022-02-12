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
    }
}
