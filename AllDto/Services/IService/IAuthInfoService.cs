using yrjw.ORM.Chimp;
using System.Threading.Tasks;
using yrjw.ORM.Chimp.Result;
using AllDto;
using AllDto.Login;
using System;
using AllModel;

namespace AllDto.Services
{
    public interface IAuthInfoService : IBaseService<AuthInfo, AuthInfoDTO, int>
    {
        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Login(LoginModel model);

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<IResultModel> CreateVerifyCode(int length = 4);

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<IResultModel> RefreshToken(string refreshToken);

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> GetAuthInfo();

    }
}
