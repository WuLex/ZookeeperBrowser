using AllModel.MyOrm;
using System.Threading.Tasks;
using AllModel.MyOrm.Result;
using AllDto;
using AllDto.Login;
using System;
using AllModel;

namespace CoreAPI.Services.IService
{
    public interface IAuthInfoService : IBaseService<AuthInfoEntity, AuthInfoDTO, int>
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