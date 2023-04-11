using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ZookeeperBrowser.Services;

namespace ZookeeperBrowser.ViewModel
{
    public class NodeViewModel
    {
        private readonly IZookeeperService _service;
        private readonly string _root;


        private string _name;

        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                //RaisePropertyChanged(() => Name);
            }
        }

        public string Path
        {
            get { return string.Format("{0}/{1}", _root, Name).Replace("//", "/").TrimEnd('/'); }
        }

        private DataViewModel _data;

        public DataViewModel Data
        {
            get { return _data; }
            private set
            {
                _data = value;
            }
        }

        private ObservableCollection<NodeViewModel> _children;

        public ObservableCollection<NodeViewModel> Children
        {
            get { return _children; }
            private set
            {
                _children = value;
            }
        }

        public NodeViewModel(IZookeeperService service, string root, string name)
        {
            _service = service;
            _root = root;
            _name = name;
            Children = new ObservableCollection<NodeViewModel>();
        }

        public async Task RefreshAsync()
        {
            Data = await _service.GetDataAsync(Path);
            var children = await _service.GetChildrenAsync(Path);
            Children = new ObservableCollection<NodeViewModel>(children);
        }
    }
}