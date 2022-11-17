using System.Collections;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZookeeperBrowser.Services;
using System.Linq;
using ZooBrowser.ViewModel;

namespace ZookeeperBrowser.ViewModel
{
    public class MainViewModel
    {
        private readonly IZookeeperService _service;

        public CnnString CnnStrings
        {
            get
            {
                //var cnnStrings = ConfigurationManager.ConnectionStrings.OfType<ConnectionStringSettings>()
                //                                                  .Select(x => new CnnString(x.Name, x.ConnectionString))
                //                                                  .ToArray();
                //SelectedCnnString = cnnStrings.FirstOrDefault();
                //return new ObservableCollection<CnnString>(cnnStrings);
                return new CnnString("", "");
            }
        }

        private CnnString _selectedCnnString;

        public CnnString SelectedCnnString
        {
            get { return _selectedCnnString; }
            set
            {
                _selectedCnnString = value;
                _service.CnnString = value != null ? value.Value : null;
                //RaisePropertyChanged(() => SelectedCnnString);
            }
        }

        private ObservableCollection<NodeViewModel> _nodes;

        public ObservableCollection<NodeViewModel> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                //RaisePropertyChanged(() => Nodes);
            }
        }

        private NodeViewModel _selectedNode;

        public NodeViewModel SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                //RaisePropertyChanged(() => SelectedNode);
            }
        }

        private ICommand _selectedItemChangedCommand;

        public ICommand SelectedItemChangedCommand
        {
            get
            {
                //return _selectedItemChangedCommand ?? new RelayCommand<RoutedPropertyChangedEventArgs<object>>(async x =>
                //{
                //    SelectedNode = x.NewValue as NodeViewModel;
                //    if (SelectedNode != null)
                //    {
                //        BusyOn();
                //        await SelectedNode.RefreshAsync();
                //        BusyOff();
                //    }
                //});

                return null;
            }
        }

        private ICommand _loadedCommand;

        public ICommand LoadedCommand
        {
            get
            {
                return _loadedCommand; //?? new RelayCommand(async () => await ReloadAsync());
            }
        }

        private ICommand _refreshCommand;

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand; //?? new RelayCommand(async () => await ReloadAsync());
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                //RaisePropertyChanged(() => IsBusy);
            }
        }

        public MainViewModel(IZookeeperService service)
        {
            _service = service;
            //MessengerInstance.Register<ExceptionOccured>(this, x => BusyOff(true));
        }

        private async Task ReloadAsync()
        {
            BusyOn();
            var nodes = await _service.GetChildrenAsync("/");
            Nodes = new ObservableCollection<NodeViewModel>(nodes);
            BusyOff();
        }


        #region [ Busy On/Off ]

        //Multiple asynchroneous processes may access IsBusy property. That's why we must have a queue to map/trace all calls and their ends.

        private Queue _busyQueue;

        private void BusyOn()
        {
            if (_busyQueue == null)
            {
                _busyQueue = new Queue();
            }

            _busyQueue.Enqueue(1);

            IsBusy = true;
        }


        private void BusyOff(bool cancelAll = false)
        {
            if (_busyQueue == null)
            {
                _busyQueue = new Queue();
            }

            if (cancelAll)
            {
                _busyQueue.Clear();
            }

            if (_busyQueue.Count > 0)
            {
                _busyQueue.Dequeue();
            }

            IsBusy = _busyQueue.Count > 0;
        }

        #endregion
    }
}