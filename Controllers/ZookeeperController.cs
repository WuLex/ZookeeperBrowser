using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.ObjectModel;
using ZookeeperBrowser.Dtos;
using ZookeeperBrowser.Services;
using ZookeeperBrowser.ViewModel;

namespace ZookeeperBrowser.Controllers
{
    public class ZookeeperController : Controller
    {
        public readonly IZookeeperService _zookeeperService;

        private readonly IConfiguration _configuration;

        public ZookeeperController(IZookeeperService zookeeperService, IConfiguration configuration)
        {
            _zookeeperService = zookeeperService;
            _configuration = configuration;
        }


        // GET: ZooController
        public ActionResult Index()
        {
            var connList = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList();
            _zookeeperService.CnnString = connList.FirstOrDefault();

            return View();
        }

        // GET: ZooController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ZooController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZooController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
         

        private async Task ReloadAsync()
        {
            BusyOn();
            var nodes = await _zookeeperService.GetChildrenAsync("/");
            Nodes = new ObservableCollection<NodeViewModel>(nodes);
            BusyOff();
        }


        public bool IsBusy
        {
            get;
            set;
        } = false;
        public ObservableCollection<NodeViewModel> Nodes { get;  set; }

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


        //public async Task<List<TreeDataModel>> GetReFunctions(int id)
        //{
        //    var trees = await GenerateTree(id);
        //    return trees;
        //}

        //public async Task<List<TreeDataModel>> GenerateTree(int id)
        //{
        //    var vm = await _companyServices.GetById(id);
        //    var reFunctions = await _presetFunctionServices.GetList();
        //    var functions = await _presetFunctionServices.GetFunctionList(id);
        //    List<TreeDataModel> trees = new List<TreeDataModel>();
        //    foreach (var reFunction in reFunctions)
        //    {
        //        var func = functions.FirstOrDefault(o => o.Code == reFunction.Code);
        //        if (func == null)
        //        {
        //            #region 生成children
        //            List<TreeDataModel> childrenTrees = new List<TreeDataModel>();
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName1))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName1", title = reFunction.OpName1, spread = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName2))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName2", title = reFunction.OpName2, spread = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName3))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName3", title = reFunction.OpName3, spread = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName4))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName4", title = reFunction.OpName4, spread = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName5))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName5", title = reFunction.OpName5, spread = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName6))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName6", title = reFunction.OpName6, spread = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName7))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName7", title = reFunction.OpName7, spread = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName8))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName8", title = reFunction.OpName8, spread = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName9))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName9", title = reFunction.OpName9, spread = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName10))
        //            {
        //                childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName10", title = reFunction.OpName10, spread = true });
        //            }
        //            #endregion
        //            trees.Add(new TreeDataModel()
        //            {
        //                id = reFunction.Code,
        //                title = reFunction.Name,
        //                children = childrenTrees.ToArray(),
        //                spread = true
        //            });
        //        }
        //        else
        //        {
        //            #region 生成children
        //            List<TreeDataModel> childrenTrees = new List<TreeDataModel>();
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName1))
        //            {
        //                if (reFunction.OpName1 == func.OpName1)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName1", title = reFunction.OpName1, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName1", title = reFunction.OpName1, spread = true });
        //                }

        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName2))
        //            {
        //                if (reFunction.OpName2 == func.OpName2)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName2", title = reFunction.OpName2, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName2", title = reFunction.OpName2, spread = true });
        //                }

        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName3))
        //            {
        //                if (reFunction.OpName3 == func.OpName3)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName3", title = reFunction.OpName3, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName3", title = reFunction.OpName3, spread = true });
        //                }

        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName4))
        //            {
        //                if (reFunction.OpName4 == func.OpName4)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName4", title = reFunction.OpName4, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName4", title = reFunction.OpName4, spread = true });
        //                }

        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName5))
        //            {
        //                if (reFunction.OpName5 == func.OpName5)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName5", title = reFunction.OpName5, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName5", title = reFunction.OpName5, spread = true });
        //                }

        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName6))
        //            {
        //                if (reFunction.OpName6 == func.OpName6)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName6", title = reFunction.OpName6, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName6", title = reFunction.OpName6, spread = true });
        //                }

        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName7))
        //            {
        //                if (reFunction.OpName7 == func.OpName7)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName7", title = reFunction.OpName7, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName7", title = reFunction.OpName7, spread = true });
        //                }

        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName8))
        //            {
        //                if (reFunction.OpName8 == func.OpName8)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName8", title = reFunction.OpName8, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName8", title = reFunction.OpName8, spread = true });
        //                }

        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName9))
        //            {
        //                if (reFunction.OpName9 == func.OpName9)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName9", title = reFunction.OpName9, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName9", title = reFunction.OpName9, spread = true });
        //                }

        //            }
        //            if (!string.IsNullOrWhiteSpace(reFunction.OpName10))
        //            {
        //                if (reFunction.OpName10 == func.OpName10)
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName10", title = reFunction.OpName10, Checked = true, spread = true });
        //                }
        //                else
        //                {
        //                    childrenTrees.Add(new TreeDataModel() { id = reFunction.Code + ",OpName10", title = reFunction.OpName10, spread = true });
        //                }

        //            }
        //            #endregion
        //            trees.Add(new TreeDataModel()
        //            {
        //                id = reFunction.Code,
        //                title = reFunction.Name,
        //                children = childrenTrees.ToArray(),
        //                spread = true
        //            });
        //        }
        //    }
        //    return trees;
        //}
    }
}
