using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AllDto;
using ZookeeperBrowser.HttpApis;
using ZookeeperBrowser.ViewModels;
//using AllDto.Common.yrjw.CommonToolsCore.Helper;
//using yrjw.ORM.Chimp.Result;

namespace ZookeeperBrowser.Controllers
{
    public class DepartController : Controller
    {
        private readonly ILogger<DepartController> _logger;
        public readonly IDepartApi _departApi;

        public DepartController(ILogger<DepartController> logger, IDepartApi departApi)
        {
            _logger = logger;
            _departApi = departApi;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        //添加页面
        [HttpGet]
        [Authorize]
        public IActionResult Create(int id)
        {
            var model = new DepartDTO();
            if (id > 0)
            {
               // model.DeptType = AllModel.Enums.EnumDeptType.classes;
                model.GradeId = id;
            }
            else
            {
               // model.DeptType = AllModel.Enums.EnumDeptType.grade;
            }
            return View(model);
        }

        //修改页面
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditAsync(int id)
        {
            var result = await _departApi.QueryAsync(id);
            if (result.Success == false)
            {
                return View();
            }
            return View(result.Data);
        }

        //表单提交，保存部门信息，id=0 添加，id>0 修改
        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SaveAsync(DepartDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IResultModel<DepartDTO> result;
        //        if (model.Id == 0)
        //        {
        //            result = await _departApi.AddAsync(model);
        //        }
        //        else
        //        {
        //            result = await _departApi.UpdateAsync(model);
        //        }
        //        if (result.Success)
        //        {
        //            var _msg = model.Id == 0 ? "添加成功！" : "修改成功！";
        //            return RedirectToAction("ShowMsg", "Home", new { msg = _msg, json = JsonHelper.SerializeJSON(result.Data) });
        //        }
        //        else
        //        {
        //            if (result.Errors.Count > 0)
        //            {
        //                ModelState.AddModelError(result.Errors[0].Id, result.Errors[0].Msg);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("error", result.Msg);
        //            }
        //        }
        //    }
        //    if (model.Id == 0)
        //    {
        //        return View("Create", model);
        //    }
        //    return View("Edit", model);
        //}

        //删除操作 ajax请求返回json
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _departApi.DeleteAsync(id);
            return Json(new Result() { success = result.Success, msg = result.Msg });
        }

        //Layui数据表格异步获取展示列表数据
        [ResponseCache(Duration = 0)]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetQueryListAsync()
        {
            var result = await _departApi.GetListAllAsync();
            if (result.Success)
            {
                return Json(new Table() { data = result.Data, count = result.Data.Count });
            }
            return Json(new Table() { data = null, count = 0 });
        }

        //Layui-dtree
        [ResponseCache(Duration = 0)]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTreeListAsync()
        {
            var result = await _departApi.GetListAllAsync();
            if (result.Success)
            {
                var list = result.Data.Select(s => new { Id = s.Id.ToString(), s.DepartName, GradeId = s.GradeId == null ? "0" : s.GradeId.ToString() });
                return Json(
                new
                {
                    status = new { code = 200, message = "操作成功" },
                    data = list
                }
            );
            }
            return Json(new NotFoundResult());
        }
    }
}
