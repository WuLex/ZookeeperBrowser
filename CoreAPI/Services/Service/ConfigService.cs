using CoreAPI.Common.Auth.Jwt;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AllDto;
using AllDto.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using AllModel.MyOrm;
using AllModel.MyOrm.Result;
using AllModel;
using CoreAPI.Services.IService;
using AllDto.Common.Auth.Jwt;
using AllDto.Common.Cache.MemoryCache;

namespace CoreAPI.Services.Service
{
    public class ConfigService : BaseService<ConfigEntity, ConfigDTO, int>, IConfigService, IDependency
    {
        public ConfigService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<ConfigService> logger, Lazy<ILoginInfo> loginInfo, Lazy<ICacheHandler> cacheHandler,
            Lazy<IRepository<ConfigEntity>> _repository) : base(mapper, unitOfWork, logger, loginInfo, cacheHandler, _repository)
        {
        }

        public async Task<IResultModel> GetValue(string code)
        {
            var _cachekey = $"{CacheKeys.CONFIG_CODE}:{code.ToUpper()}";
            if (!_cacheHandler.Value.TryGetValue(_cachekey, out ConfigDTO configDTO))
            {
                var entity = await _repository.Value.TableNoTracking.FirstOrDefaultAsync(p => p.Code.ToUpper() == code.ToUpper());
                if (entity == null)
                {
                    _logger.LogError($"error：entity Code {code} does not exist");
                    return ResultModel.NotExists;
                }
                configDTO = _mapper.Value.Map<ConfigDTO>(entity);
                //加入缓存
                await _cacheHandler.Value.SetAsync(_cachekey, configDTO);
            }
            return ResultModel.Success(configDTO);
        }

        public async Task<IResultModel> SetValue(ConfigDTO model)
        {
            var entity = _repository.Value.TableNoTracking.FirstOrDefault(p => p.Code.ToUpper() == model.Code.ToUpper());
            if (entity == null)
            {
                return ResultModel.NotExists;
            }
            var configDTO = _mapper.Value.Map<ConfigDTO>(entity);

            #region 校验json格式
            try
            {
                switch (model.Code.ToUpper())
                {
                    case "AUTH":
                        model.Value.ToJson<AuthConfigData>();
                        break;
                }
            }
            catch (Exception)
            {
                return ResultModel.Failed("error ToJson DeserializeObject");
            }
            #endregion

            //删除配置信息缓存
            await _cacheHandler.Value.RemoveAsync($"{CacheKeys.CONFIG_CODE}:{model.Code.ToUpper()}");

            configDTO.Value = model.Value;
            return await base.UpdateAsync(configDTO);
        }
    }
}
