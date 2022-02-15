using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Student.DTO;
using ZookeeperBrowser.Code;
using ZookeeperBrowser.HttpApis;
using ZookeeperBrowser.ViewModels;
using yrjw.ORM.Chimp.Result;
using ZookeeperBrowser.Common.Auth.Jwt;

namespace ZookeeperBrowser.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ILoginInfo _loginInfo;
        public readonly IAccountApi _accountApi;

        public AccountController(ILogger<AccountController> logger, ILoginInfo loginInfo, IAccountApi accountApi)
        {
            _logger = logger;
            _loginInfo = loginInfo;
            _accountApi = accountApi;
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
        public IActionResult Create()
        {
            return View();
        }

        //修改页面
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var result = await _accountApi.QueryAsync(id);
            if (result.Success == false)
            {
                return View();
            }
            return View(result.Data);
        }

        //表单提交，保存账户信息，id=0 添加，id>0 修改
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAsync(AccountDTO model)
        {
            if (ModelState.IsValid)
            {
                IResultModel result;
                if (model.Id == Guid.Empty)
                {
                    result = await _accountApi.AddAsync(model);
                }
                else
                {
                    result = await _accountApi.UpdateAsync(model);
                }
                if (result.Success)
                {
                    var _msg = model.Id == Guid.Empty ? "添加成功！" : "修改成功！";
                    return RedirectToAction("ShowMsg", "Home", new { msg = _msg });
                }
                else
                {
                    if (result.Errors.Count > 0)
                    {
                        ModelState.AddModelError(result.Errors[0].Id, result.Errors[0].Msg);
                    }
                    else
                    {
                        ModelState.AddModelError("error", result.Msg);
                    }
                }
            }
            if (model.Id == Guid.Empty)
            {
                return View("Create", model);
            }
            return View("Edit", model);
        }

        //删除操作 ajax请求返回json
        [Authorize]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _accountApi.DeleteAsync(id);
            return Json(new Result() { success = result.Success, msg = result.Msg });
        }

        //Layui数据表格异步获取展示列表数据
        [ResponseCache(Duration = 0)]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetQueryListAsync()
        {
            var result = await _accountApi.GetListAllAsync();
            if (result.Success)
            {
                return Json(new Table() { data = result.Data, count = result.Data.Count });
            }
            return Json(new Table() { data = null, count = 0 });
        }
    }
}
