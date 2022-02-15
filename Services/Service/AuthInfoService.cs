using ZookeeperBrowser.Common.Auth.Jwt;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Student.DTO;
using Student.DTO.Cache;
using Student.DTO.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yrjw.ORM.Chimp;
using yrjw.ORM.Chimp.Result;
using ZookeeperBrowser.Code;
using ZookeeperBrowser.Common.Auth.Jwt;
using ZookeeperBrowser.Common.Cache.MemoryCache;
using ZookeeperBrowser.Common.CommonToolsCore.Extensions;
using ZookeeperBrowser.Common.CommonToolsCore.Helper;
using ZookeeperBrowser.Model;
using ZookeeperBrowser.Model.Enums;
using ZookeeperBrowser.Services.IService;

namespace ZookeeperBrowser.Services.Service
{
    public class AuthInfoService : BaseService<AuthInfo, AuthInfoDTO, int>, IAuthInfoService, IDependency
    {
        private readonly Lazy<IRepository<Account>> repAccount;

        public AuthInfoService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<AuthInfoService> logger, Lazy<ICacheHandler> cacheHandler,
            Lazy<IRepository<AuthInfo>> _repository, Lazy<ILoginInfo> loginInfo, Lazy<IRepository<Account>> repAccount) : base(mapper, unitOfWork, logger, loginInfo, cacheHandler, _repository)
        {
            this.repAccount = repAccount;
        }

        public async Task<IResultModel> Login(LoginModel model)
        {
            //检测验证码
            if (AuthConfigData.AuthConfig.VerifyCode)
            {
                var verifyCodeCheckResult = CheckVerifyCode(model);
                if (!verifyCodeCheckResult.Success)
                    return verifyCodeCheckResult;
            }

            //检查账户密码
            var entity = await repAccount.Value.TableNoTracking.FirstAsync(p => p.UserName == model.UserName.Trim());
            if (entity == null)
            {
                return ResultModel.Failed("用户名密码错误"); //用户不存在
            }

            var _passWord = $"{model.UserName}_{model.Password}".ToMd5Hash();
            if (!_passWord.Equals(entity.PassWord))
            {
                return ResultModel.Failed("用户名密码错误");
            }

            //更新认证信息并返回登录结果
            var resultModel = await UpdateAuthInfo(entity, model);
            if (resultModel != null)
            {
                return ResultModel.Success(resultModel);
            }
            return ResultModel.Failed("更新认证信息失败");
        }

        public async Task<IResultModel> CreateVerifyCode(int length = 4)
        {
            var model = new VerifyCodeModel
            {
                Id = Guid.NewGuid().ToString(),
                Code = ValidateCodeHelper.CreateBase64String(out string code, length)
            };

            //把验证码放到内存缓存中，有效期10分钟
            var key = $"{CacheKeys.VERIFY_CODE}:{model.Id}";
            await _cacheHandler.Value.SetAsync(key, code, 10);

            return ResultModel.Success(model);
        }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<IResultModel> RefreshToken(string refreshToken)
        {
            var _cachekey = $"{CacheKeys.AUTH_REFRESH_TOKEN}:{refreshToken}";
            if (!_cacheHandler.Value.TryGetValue(_cachekey, out AuthInfoDTO authInfoDTO))
            {
                var authInfo = await _repository.Value.TableNoTracking.FirstOrDefaultAsync(p => p.RefreshToken == refreshToken);
                if (authInfo == null)
                    return ResultModel.Failed("身份认证信息无效，请重新登录");
                authInfoDTO = _mapper.Value.Map<AuthInfoDTO>(authInfo);

                //加入缓存
                var expires = (int)(authInfo.RefreshTokenExpiredTime - DateTime.Now).TotalMinutes;
                await _cacheHandler.Value.SetAsync(_cachekey, authInfoDTO, expires);
            }

            if (authInfoDTO.RefreshTokenExpiredTime <= DateTime.Now)
                return ResultModel.Failed("身份认证信息过期，请重新登录~");

            var account = await repAccount.Value.GetByIdAsync(authInfoDTO.AccountId);
            if (account == null)
                return ResultModel.Failed("账户信息不存在");

            var accountDTO = _mapper.Value.Map<AccountDTO>(account);
            if (account.Status != EnumStatus.Enabled)
                return ResultModel.Failed($"账户状态：{accountDTO.StatusName}");
            
            return ResultModel.Success(new LoginResultModel
            {
                Account = accountDTO,
                AuthInfo = authInfoDTO
            });
        }

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <returns></returns>
        public async Task<IResultModel> GetAuthInfo()
        {
            var _cachekey = $"{CacheKeys.AUTH_INFO}:{_loginInfo.Value.AccountId}:{_loginInfo.Value.Platform}";
            if (!_cacheHandler.Value.TryGetValue(_cachekey, out AuthInfoDTO authInfoDTO))
            {
                var authInfo = await _repository.Value.TableNoTracking.FirstOrDefaultAsync(p => p.AccountId == _loginInfo.Value.AccountId && p.Platform == (EnumPlatform)_loginInfo.Value.Platform);
                if (authInfo == null)
                    return ResultModel.Failed("身份认证信息无效，请重新登录");
                authInfoDTO = _mapper.Value.Map<AuthInfoDTO>(authInfo);

                //加入缓存
                var expires = (int)(authInfo.RefreshTokenExpiredTime - DateTime.Now).TotalMinutes;
                await _cacheHandler.Value.SetAsync(_cachekey, authInfoDTO, expires);
            }

            if (authInfoDTO.RefreshTokenExpiredTime <= DateTime.Now)
                return ResultModel.Failed("身份认证信息过期，请重新登录~");

            var account = await repAccount.Value.GetByIdAsync(authInfoDTO.AccountId);
            if (account == null)
                return ResultModel.Failed("账户信息不存在");

            var accountDTO = _mapper.Value.Map<AccountDTO>(account);
            if (account.Status != EnumStatus.Enabled)
                return ResultModel.Failed($"账户状态：{accountDTO.StatusName}");

            return ResultModel.Success(new LoginResultModel
            {
                Account = accountDTO,
                AuthInfo = authInfoDTO
            });
        }

        private IResultModel CheckVerifyCode(LoginModel model)
        {
            if (model.VerifyCode == null || model.VerifyCode.Code.IsNull())
                return ResultModel.Failed("请输入验证码");

            if (model.VerifyCode.Id.IsNull())
                return ResultModel.Failed("验证码不存在");

            var cacheCode = _cacheHandler.Value.GetAsync($"{CacheKeys.VERIFY_CODE}:{model.VerifyCode.Id}").Result;
            if (cacheCode.IsNull())
                return ResultModel.Failed("验证码不存在");

            if (!cacheCode.Equals(model.VerifyCode.Code))
                return ResultModel.Failed("验证码有误");
            return ResultModel.Success();
        }

        /// <summary>
        /// 更新账户认证信息
        /// </summary>
        private async Task<LoginResultModel> UpdateAuthInfo(Account account, LoginModel model)
        {
            var accountDTO = _mapper.Value.Map<AccountDTO>(account);
            var authInfo = new AuthInfoDTO
            {
                AccountId = account.Id,
                Platform = model.Platform,
                LoginTime = DateTime.Now.ToTimestamp(),
                LoginIP = model.IP,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiredTime = DateTime.Now.AddDays(7)//默认刷新令牌有效期7天
            };

            IResultModel result;
            var entity = _repository.Value.TableNoTracking.FirstOrDefault(p => p.AccountId == account.Id && p.Platform == model.Platform);
            if (entity != null)
            {
                authInfo.Id = entity.Id;
                result = await base.UpdateAsync(authInfo);
            }
            else
            {
                result = await base.InsertAsync(authInfo);
            }

            if (result.Success)
            {
                //删除验证码缓存
                await _cacheHandler.Value.RemoveAsync($"{CacheKeys.VERIFY_CODE}:{model.VerifyCode.Id}");

                //删除认证信息缓存
                await _cacheHandler.Value.RemoveAsync($"{CacheKeys.AUTH_INFO}:{account.Id}:{model.Platform.ToInt()}");

                return new LoginResultModel
                {
                    Account = accountDTO,
                    AuthInfo = authInfo
                };
            }
            return null;
        }

        /// <summary>
        /// 生成刷新令牌
        /// </summary>
        /// <returns></returns>
        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
