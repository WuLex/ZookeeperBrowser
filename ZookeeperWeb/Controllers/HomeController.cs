using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZookeeperBrowser.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZookeeperBrowser.HttpApis;
using AllModel.Enums;

namespace ZookeeperBrowser.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IAuthApi _authApi;

        public HomeController(ILogger<HomeController> logger, IAuthApi authApi)
        {
            _logger = logger;
            _authApi = authApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MainPC()
        {
            var _info = await _authApi.AuthInfo();
            if (_info.Success)
            {
                ViewBag.Name = _info.Data.Account.Name;
                if (_info.Data.AuthInfo.Platform == EnumPlatform.Web)
                {
                    return View();
                }
            }
            return View();
        }

        /// <summary>
        /// 弹窗子窗口，保存后刷新父级页面数据表格
        /// </summary>
        /// <param name="msg">弹窗提示信息</param>
        /// <param name="json">不为空时，只刷新本地数据</param>
        /// <returns></returns>
        public IActionResult ShowMsg(string msg = "保存成功！", string json = "")
        {
            ViewBag.Msg = msg;
            ViewBag.Data = json;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}