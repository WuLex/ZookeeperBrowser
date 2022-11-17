using AllDto;
using AllDto.Login;
using ZookeeperBrowser.Code;
using ZookeeperBrowser.ViewModels;
using WebApiClient;
using WebApiClient.Attributes;
using AllModel.MyOrm.Result;

namespace ZookeeperBrowser.HttpApis
{
    [TokenFilter]
    [JsonReturn]
    public interface IAuthApi : IHttpApi
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/Auth/VerifyCode")]
        ITask<ResultModel<VerifyCodeModel>> GetVerifyCode(int length);

        /// <summary>
        /// 登录处理
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/Auth/Login")]
        ITask<ResultModel<JwtTokenModel>> Login([JsonContent] LoginModel model);

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/Auth/RefreshToken")]
        ITask<ResultModel<JwtTokenModel>> RefreshToken(string refreshToken);

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/Auth/AuthInfo")]
        ITask<ResultModel<LoginResultModel>> AuthInfo();
    }
}