using AllDto.Common.Auth.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.ObjectModel;
using System.Net;
using ZookeeperBrowser.Dtos;
using ZookeeperBrowser.Models;
using ZookeeperBrowser.Services;
using ZookeeperBrowser.ViewModel;

namespace ZookeeperBrowser.Controllers
{

    [Authorize]
    public class ZookeeperController : Controller
    {
        public readonly IZookeeperService _zookeeperService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ZookeeperController(IZookeeperService zookeeperService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _zookeeperService = zookeeperService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public ActionResult IndexOne()
        {
            return View();
        }

        public ActionResult Index()
        {
           ViewBag.AccountName = _httpContextAccessor.HttpContext.User.FindFirst(nameof(ClaimsName.AccountName)).Value;

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

        /// <summary>
        /// 无法异步加载，弃用
        /// </summary>
        /// <param name="nodepath"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<TreeDataModel>> GetLayuiTreeData(string nodepath = "/")
        {
            string nodePath = WebUtility.HtmlDecode(nodepath);
            TreeDataModel treeDataModel = null;
            var connList = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList();
            _zookeeperService.CnnString = connList.FirstOrDefault() ?? "127.0.0.1";
            if (nodepath == "/")
            {
                treeDataModel = new TreeDataModel()
                {
                    id = "/",
                    title = "Root",
                    disabled = true,
                };
                var nodes = await _zookeeperService.GetChildrenAsync(nodePath);
                List<TreeDataModel> childdatalist = new List<TreeDataModel>();
                if (nodes != null)
                {
                    var nodelist = nodes.ToList();
                    for (int i = 0; i < nodelist.Count(); i++)
                    {
                        childdatalist.Add(new TreeDataModel()
                        {
                            id = Convert.ToString(i + 1),
                            title = nodelist[i].Name,
                            href = nodelist[i].Path,
                        });
                    }
                }
                treeDataModel.children = childdatalist;
                List<TreeDataModel> treeDatalist = new List<TreeDataModel>();
                treeDatalist.Add(treeDataModel);
                return treeDatalist;
            }
            else
            {
                var nodechilds = await _zookeeperService.GetChildrenAsync(nodePath);
                List<TreeDataModel> childdatalist = new List<TreeDataModel>();
                if (nodechilds != null)
                {
                    var nodechildlist = nodechilds.ToList();
                    for (int i = 0; i < nodechildlist.Count(); i++)
                    {
                        childdatalist.Add(new TreeDataModel()
                        {
                            id = Convert.ToString(i + 1),
                            title = nodechildlist[i].Name,
                            href = nodechildlist[i].Path,
                        });
                    }
                }
                return childdatalist;
            }
        }

        /// <summary>
        /// 获取树状节点
        /// </summary>
        /// <param name="nodepath"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ZTreeDataModel>> GetZTreeData(string Id)
        {
            string nodePath = WebUtility.HtmlDecode(Id);
            ZTreeDataModel treeDataModel = null;
            List<ZTreeDataModel> datalist = new List<ZTreeDataModel>();

            var connList = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList();
            _zookeeperService.CnnString = connList.FirstOrDefault() ?? "127.0.0.1";

            if (string.IsNullOrEmpty(Id))
            {
                List<ZTreeDataModel> treeDatalist = new List<ZTreeDataModel>();
                treeDataModel = new ZTreeDataModel()
                {
                    Id = "/",
                    Name = "Root",
                    IsParent = true,
                    open = true,
                };
                datalist.Add(treeDataModel);
            }
            else if (Id == "/")
            {
                var childnodes = await _zookeeperService.GetChildrenAsync(nodePath);

                if (childnodes != null)
                {
                    var childnodelist = childnodes.ToList();
                    for (int i = 0; i < childnodelist.Count(); i++)
                    {
                        //获取该节点路径状态信息
                        var nodeInfo = await _zookeeperService.GetDataAsync(childnodelist[i].Path);
                        datalist.Add(new ZTreeDataModel()
                        {
                            Id = childnodelist[i].Path,
                            Pid = "/",
                            Name = childnodelist[i].Name,
                            IsParent = nodeInfo.NumChildren > 0
                        });
                    }
                }
            }
            else
            {
                var childnodes = await _zookeeperService.GetChildrenAsync(nodePath);
               
                if (childnodes != null)
                {
                    var childnodelist = childnodes.ToList();
                    if (childnodelist.Count > 0)
                    {
                        for (int i = 0; i < childnodelist.Count(); i++)
                        {
                            //获取该节点路径状态信息
                            var nodeInfo = await _zookeeperService.GetDataAsync(childnodelist[i].Path);
                            datalist.Add(new ZTreeDataModel()
                            {
                                Id = childnodelist[i].Path,
                                Name = childnodelist[i].Name,
                                Pid = nodePath,
                                IsParent = nodeInfo.NumChildren > 0,
                            });
                        }
                    }
                }
            }
            return datalist;
        }

        /// <summary>
        /// 获取树状节点
        /// </summary>
        /// <param name="nodepath"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageDataResult<DataViewModel>> GetDataViewModel([FromBody] ZKQueryData zKQueryData)
        {
            string nodePath = WebUtility.HtmlDecode(zKQueryData.nodepath);

            var connList = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList();
            _zookeeperService.CnnString = connList.FirstOrDefault() ?? "127.0.0.1";
            var dataViewModel = await _zookeeperService.GetDataAsync(nodePath);
            //return dataViewModel;

            var list = new List<DataViewModel>();
            if (dataViewModel != null)
            {
                list.Add(dataViewModel);
            }
            return new PageDataResult<DataViewModel>()
            {
                Msg = "success",
                Code = 0,
                Count = list.Count,
                Data = list
            };
        }

        //生成树的方法
        public void GetTheTree(TreeDataModel dataModel)
        {
            var connList = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList();
            _zookeeperService.CnnString = connList.FirstOrDefault() ?? "127.0.0.1";
            //获取
            var nodes = _zookeeperService.GetChildrenAsync("/").Result;
            //如果没有字节点了，那就返回空
            if (nodes != null)
            {
                var nodelist = nodes.ToList();

                List<TreeDataModel> childdatalist = new List<TreeDataModel>();
                for (int i = 0; i < nodelist.Count(); i++)
                {
                    var childnode = new TreeDataModel()
                    {
                        id = Convert.ToString(i + 1),
                        title = nodelist[i].Name,
                        href = nodelist[i].Path,
                    };
                    //递归循环
                    GetTheTree(childnode);
                    childdatalist.Add(childnode);
                }
                dataModel.children = childdatalist;
            }
            else
            {
                return;
            }
        }

        private async Task ReloadAsync()
        {
            var connList = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList();
            _zookeeperService.CnnString = connList.FirstOrDefault() ?? "127.0.0.1";
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

        public ObservableCollection<NodeViewModel> Nodes { get; set; }

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