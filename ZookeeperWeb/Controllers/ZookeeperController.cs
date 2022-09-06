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
            //var connList = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList();
            //_zookeeperService.CnnString = connList.FirstOrDefault() ?? "127.0.0.1";
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

            //var connList = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList();
            //_zookeeperService.CnnString = connList.FirstOrDefault() ?? "127.0.0.1";

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

        #region 删除节点

        [HttpPost]
        public async Task<bool> DeleteNodeAsync([FromBody] ZKQueryData zKQueryData)
        {
            string nodePath = WebUtility.HtmlDecode(zKQueryData.nodepath);
            try
            {
                var deleteFlag = await _zookeeperService.DeleteRecursiveAsync(nodePath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion 删除节点

        #region 刷新

        private async Task ReloadAsync()
        {
            //var connList = _configuration["ZooKeeperConn:ConnectionString"].Split(",").ToList();
            //_zookeeperService.CnnString = connList.FirstOrDefault() ?? "127.0.0.1";
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

        #endregion 刷新
    }
}