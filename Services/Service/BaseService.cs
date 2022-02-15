using ZookeeperBrowser.Common.Auth.Jwt;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ZookeeperBrowser.Common.Cache.MemoryCache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yrjw.ORM.Chimp;
using yrjw.ORM.Chimp.Result;
using ZookeeperBrowser.Services.IService;
using ZookeeperBrowser.Model.Code;

namespace ZookeeperBrowser.Services.Service
{
    public class BaseService<TEntity, TEntityDTO, TKey> : IBaseService<TEntity, TEntityDTO, TKey> where TEntityDTO : class where TEntity : EntityBase<TKey>, new() where TKey: struct
    {
        protected readonly ILogger<BaseService<TEntity, TEntityDTO, TKey>> _logger;
        protected readonly Lazy<IMapper> _mapper;
        protected readonly Lazy<ICacheHandler> _cacheHandler;
        protected readonly Lazy<ILoginInfo> _loginInfo;

        /// <summary>
        /// TEntity仓储
        /// </summary>
        protected readonly Lazy<IRepository<TEntity>> _repository;
        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        public BaseService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<BaseService<TEntity, TEntityDTO, TKey>> logger, Lazy<ILoginInfo> loginInfo,
           Lazy<IRepository<TEntity>> repository)
        {
            _logger = logger;
            _mapper = mapper;
            _loginInfo = loginInfo;
            UnitOfWork = unitOfWork;
            this._repository = repository;
        }

        public BaseService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<BaseService<TEntity, TEntityDTO, TKey>> logger, Lazy<ILoginInfo> loginInfo, Lazy<ICacheHandler> cacheHandler, 
            Lazy<IRepository<TEntity>> repository)
        {
            _logger = logger;
            _mapper = mapper;
            _loginInfo = loginInfo;
            _cacheHandler = cacheHandler;
            UnitOfWork = unitOfWork;
            this._repository = repository;
        }

        public virtual async Task<IResultModel> GetByIdAsync(TKey id)
        {
            var info = await _repository.Value.GetByIdAsync(id);
            return ResultModel.Success(_mapper.Value.Map<TEntityDTO>(info));
        }

        public virtual async Task<IResultModel> GetListAllAsync(bool isDescending = false)
        {
            if (isDescending) {
                var Descendinglist = await _repository.Value.TableNoTracking.OrderByDescending(k => k.Id).ProjectTo<TEntityDTO>(_mapper.Value.ConfigurationProvider).ToListAsync();
                return ResultModel.Success(Descendinglist);
            }
            var list = await _repository.Value.TableNoTracking.OrderBy(k => k.Id).ProjectTo<TEntityDTO>(_mapper.Value.ConfigurationProvider).ToListAsync();
            return ResultModel.Success(list);
        }

        public virtual async Task<IResultModel> InsertAsync(TEntityDTO model)
        {
            var entity = _mapper.Value.Map<TEntity>(model);
            entity.CreatedTime = DateTime.Now;
            entity.ModifiedTime = DateTime.Now;
            if (_loginInfo != null && _loginInfo.Value != null)
            {
                entity.OperatorName = _loginInfo.Value.AccountName;
            }
            await _repository.Value.InsertAsync(entity);

            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success(_mapper.Value.Map<TEntityDTO>(entity));
            }
            _logger.LogError($"error：Insert Save failed");
            return ResultModel.Failed("error：Insert Save failed", 500);
        }

        public virtual async Task<IResultModel> UpdateAsync(TEntityDTO model)
        {
            //主键判断
            var entity = await _repository.Value.GetByIdAsync(((dynamic)model).Id);
            if (entity == null)
            {
                _logger.LogError($"error：entity Id {((dynamic)model).Id} does not exist");
                return ResultModel.NotExists;
            }
            _mapper.Value.Map(model, entity);
            entity.ModifiedTime = DateTime.Now;
            if (_loginInfo != null && _loginInfo.Value != null)
            {
                entity.OperatorName = _loginInfo.Value.AccountName;
            }
            _repository.Value.Update(entity);

            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success(entity);
            }
            _logger.LogError($"error：Update Save failed");
            return ResultModel.Failed("error：Update Save failed", 500);
        }

        public virtual async Task<IResultModel> UpdateAsync(IEnumerable<TEntityDTO> models)
        {
            var entitys = new List<TEntity>();
            foreach (var model in models)
            {
                //主键判断
                var entity = await _repository.Value.GetByIdAsync(((dynamic)model).Id);
                if (entity == null)
                {
                    _logger.LogError($"error：entity Id {((dynamic)model).Id} does not exist");
                    return ResultModel.NotExists;
                }
                entitys.Add(entity);
            }
            _repository.Value.Update(entitys);

            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success();
            }
            _logger.LogError($"error：Updates Save failed");
            return ResultModel.Failed("error：Updates Save failed", 500);
        }

        public virtual async Task<IResultModel> DeleteAsync(TKey id)
        {
            //主键判断
            var entity = await _repository.Value.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogError($"error：entity Id：{id} does not exist");
                return ResultModel.NotExists;
            }
            //判断模型是否拥有软删除字段
            if(entity is EntityBaseNoDeleted<TKey>)
            {
                _logger.LogError($"error：not inheritance for EntityBaseNoDeleted");
                return ResultModel.Failed("error：not inheritance for EntityBaseNoDeleted", 500);
            }
            //软删除
            if (entity.Deleted == 0)
            {
                entity.Deleted = 1;
                _repository.Value.Update(entity);
            }
            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success();
            }
            _logger.LogError($"error：Delete failed");
            return ResultModel.Failed("error：Delete failed", 500);
        }

        public virtual async Task<IResultModel> DeleteAsync(IList<TKey> ids)
        {
            foreach (var id in ids)
            {
                //主键判断
                var entity = await _repository.Value.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogError($"error：entity Id：{id} does not exist");
                    return ResultModel.NotExists;
                }
                //软删除
                if (entity.Deleted == 0)
                {
                    entity.Deleted = 1;
                    _repository.Value.Update(entity);
                } 
            }
            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success();
            }
            _logger.LogError($"error：Delete failed");
            return ResultModel.Failed("error：Delete failed", 500);
        }

        public virtual async Task<IResultModel> RemoveAsync(TKey id)
        {
            //主键判断
            var entity = await _repository.Value.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogError($"error：entity Id：{id} does not exist");
                return ResultModel.NotExists;
            }
            _repository.Value.Delete(entity);
            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success();
            }
            _logger.LogError($"error：Remove failed");
            return ResultModel.Failed("error：Remove failed", 500);
        }

        public virtual async Task<IResultModel> RemoveAsync(IList<TKey> ids)
        {
            foreach (var id in ids)
            {
                //主键判断
                var entity = await _repository.Value.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogError($"error：entity Id：{id} does not exist");
                    return ResultModel.NotExists;
                }
                _repository.Value.Delete(entity);
            }
            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success();
            }
            _logger.LogError($"error：Remove failed");
            return ResultModel.Failed("error：Remove failed", 500);
        }
    }
}
