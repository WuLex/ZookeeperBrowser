using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AllDto.Login;
using ZookeeperBrowser.HttpApis;
using ZookeeperBrowser.ViewModels;
using AllModel.Enums;
using AllDto.Common.CommonToolsCore.Helper;

namespace ZookeeperBrowser.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        public readonly IAuthApi _authApi;

        public LoginController(ILogger<LoginController> logger, IAuthApi authApi)
        {
            _logger = logger;
            _authApi = authApi;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userid = TempData["userid"];
            if (userid != null)
            {
                return View(new LoginViewModel() { userid = userid.ToString() });
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                var id = TempData["VerifyCode_ID"];
                if (id == null)
                {
                    ModelState.AddModelError("error", "验证码错误，请刷新验证码！");
                }

                var model = new LoginModel();
                model.UserName = vModel.userid;
                model.Password = vModel.password;
                model.Platform = EnumPlatform.Web;
                model.VerifyCode = new VerifyCodeModel() { Id = id.ToString(), Code = vModel.code };

                //获取ip地址
                model.IP = HttpContext.Connection.RemoteIpAddress.ToString() ?? "";
                //获取UserAgent
                model.UserAgent = HttpContext.Request.Headers["User-Agent"].FirstOrDefault() ?? "";

                var result = await _authApi.Login(model);
                if (result.Success)
                {
                    SaveUserCookie(result.Data); //登录成功
                }
                else
                {
                    ModelState.AddModelError("error", result.Msg);
                }
            }

            return View(vModel);
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetValidataCodeAsync()
        {
            try
            {
                var result = await _authApi.GetVerifyCode(4);
                if (result.Success)
                {
                    TempData["VerifyCode_ID"] = result.Data.Id;
                    var imgValidateCode =
                        Convert.FromBase64String(result.Data.Code.Replace("data:image/png;base64,", ""));
                    return File(imgValidateCode, "image/jpeg");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return File(ValidateCodeHelper.CreateValidateGraphic(""), "image/jpeg");
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LoginOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        /// <summary>
        /// ASP.NET CORE Cookie 保存身份信息
        /// </summary>
        private void SaveUserCookie(JwtTokenModel jwt)
        {
            //创建 Claim 对象将用户信息存储在 Claim 类型的字符串键值对中，
            //将 Claim 对象传入 ClaimsIdentity 中，用来构造一个 ClaimsIdentity 对象
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim("AccessToken", jwt.AccessToken, ClaimValueTypes.String));
            identity.AddClaim(new Claim("RefreshToken", jwt.RefreshToken, ClaimValueTypes.String));
            identity.AddClaim(new Claim("ExpiresIn", jwt.ExpiresIn.ToString(), ClaimValueTypes.Integer32));
            identity.AddClaim(new Claim("AccountName", jwt.AccountName.ToString(), ClaimValueTypes.String));

            //调用 HttpContext.SignInAsync 方法，传入上面创建的 ClaimsPrincipal 对象，完成用户登录
            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddHours(6) });

            //可以使用 HttpContext.SignInAsync 方法的重载来定义持久化 cookie 存储用户认证信息，如下定义了用户登录 60 分钟内cookie 都会保留在客户端计算机硬盘上
            //即使关闭浏览器，60 分钟内再次访问仍然是处于登录状态，除非调用 Logout 方法注销登录
            //注意 AllowRefresh 属性，如果 AllowRefresh 为 true， 表示如果用户登录超过 50% 的 ExpiresUtc 时间间隔内又访问了站点，就延长用户的登录时间（其实就是延长 cookie 客户端计算机硬盘的保留时间）。
            //如下，设置 ExpiresUtc 属性为 60分钟后，那么当用户登录在大于 30 分钟且小于 60 分钟内访问了站点，那么就将用户登录状态再延长到当前时间后的 60 分钟。但用户在登录后 30 分钟内访问站点是不会延长登录时间的。
            //因为 ASP.NET Core 有个硬核要求，就是用户在超过 50% 的 ExpiresUtc 时间间隔内又访问了站点，才延长用户的登录时间。
            //如果 AllowRefresh 为 false，表示用户登陆后 60 分钟内不管有没有访问站点，只要 60 分钟到了，立马就处于非登录状态（不延长 cookie 在客户端计算机硬盘上的保留时间，60 分钟到了，客户端计算机自动删除 cookie）


            //调用 HttpContext.SignInAsync 方法，传入上面创建的 ClaimsPrincipal 对象，完成用户登录
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    //获取或设置身份验证会话是否跨多个持久化要求
                    IsPersistent = false,
                    ExpiresUtc = null,
                    //AllowRefresh = true,
                    RedirectUri = "/Home/Index"
                });

            //如果当前 Http 请求本来登录了用户 A，现在调用 HttpContext.SignInAsync 方法登录用户 B，那么相当于注销用户 A，登录用户 B
        }
    }
}