using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AllDto.Common.Auth.Jwt;
using AllDto.Login;
using AllDto.Services;
using AllDto.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using AllModel.MyOrm.Result;

namespace CoreAPI.Controllers
{
    [Description("身份认证")]
    public class AuthController : ControllerAbstract
    {
        private readonly ILoginHandler _loginHandler;
        private readonly IpHelper _ipHelper;
        private readonly Lazy<IAuthInfoService> AuthInfoService;

        public AuthController(ILogger<ControllerAbstract> logger, ILoginHandler loginHandler, IpHelper ipHelper, Lazy<IAuthInfoService> authInfoService) : base(logger)
        {
            _loginHandler = loginHandler;
            _ipHelper = ipHelper;
            AuthInfoService = authInfoService;
        }

        [HttpGet("VerifyCode")]
        [AllowAnonymous]
        [Description("获取验证码")]
        public async Task<IResultModel> VerifyCode(int length = 4)
        {
            return await AuthInfoService.Value.CreateVerifyCode(length);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [Description("用户名登录")]
        public async Task<IResultModel> Login(LoginModel model)
        {
            model.IP = _ipHelper.IP;
            model.UserAgent = _ipHelper.UserAgent;
            var result = await AuthInfoService.Value.Login(model);
            return LoginHandle(result);
        }

        /// <summary>
        /// 登录处理
        /// </summary>
        private IResultModel LoginHandle(IResultModel result)
        {
            if (result.Success)
            {
                var model = result as IResultModel<LoginResultModel>;
                var account = model.Data.Account;
                var loginInfo = model.Data.AuthInfo;
                var claims = new List<Claim>
                {
                    new Claim(ClaimsName.AccountId, account.Id.ToString()),
                    new Claim(ClaimsName.AccountName, account.Name),
                    new Claim(ClaimsName.AccountType, account.Type.ToInt().ToString()),
                    new Claim(ClaimsName.Platform, loginInfo.Platform.ToInt().ToString()),
                    new Claim(ClaimsName.LoginTime, loginInfo.LoginTime.ToString())
                };
                var jwtmodel = _loginHandler.Hand(claims, loginInfo.RefreshToken);
                return ResultModel.Success(jwtmodel);
            }
            return ResultModel.Failed(result.Msg);
        }

        [HttpGet("RefreshToken")]
        [AllowAnonymous]
        [Description("刷新令牌")]
        public async Task<IResultModel> RefreshToken([BindRequired]string refreshToken)
        {
            var result = await AuthInfoService.Value.RefreshToken(refreshToken);
            return LoginHandle(result);
        }

        [HttpGet("AuthInfo")]
        [Description("获取认证信息")]
        public async Task<IResultModel> AuthInfo()
        {
            return await AuthInfoService.Value.GetAuthInfo();
        }
    }
}
