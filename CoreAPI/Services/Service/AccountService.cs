using CoreAPI.Common.Auth.Jwt;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AllDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllModel;
using AllModel.MyOrm;
using AllModel.MyOrm.Result;
using CoreAPI.Services.IService;
using AllDto.Common.Auth.Jwt;

namespace CoreAPI.Services.Service
{
    public class AccountService : BaseService<AccountEntity, AccountDTO, Guid>, IAccountService, IDependency
    {
        public AccountService(Lazy<IMapper> mapper, IUnitOfWork unitOfWork, ILogger<AccountService> logger,
            Lazy<ILoginInfo> loginInfo,
            Lazy<IRepository<AccountEntity>> _repository) : base(mapper, unitOfWork, logger, loginInfo, _repository)
        {
        }

        public override async Task<IResultModel> InsertAsync(AccountDTO model)
        {
            //检查用户名是否唯一
            if (model.UserName.NotNull())
            {
                var isusername =
                    await _repository.Value.TableNoTracking.AnyAsync(p => p.UserName == model.UserName.Trim());
                if (isusername)
                {
                    _logger.LogError($"error：UserName {model.UserName} already exists");
                    return ResultModel.Failed("用户名已存在", "UserName");
                }
            }

            //默认密码
            if (model.PassWord.IsNull())
            {
                model.PassWord = "123456";
            }

            //调用父类方法
            return await base.InsertAsync(model);
        }

        public override async Task<IResultModel> UpdateAsync(AccountDTO model)
        {
            //检查用户名是否唯一
            if (model.UserName.NotNull())
            {
                var isusername =
                    await _repository.Value.TableNoTracking.AnyAsync(p =>
                        p.UserName == model.UserName.Trim() && p.Id != model.Id);
                if (isusername)
                {
                    _logger.LogError($"error：UserName {model.UserName} already exists");
                    return ResultModel.Failed("用户名已存在", "UserName");
                }
            }

            //密码空则保留原密码
            if (model.PassWord.IsNull())
            {
                var entity = await _repository.Value.GetByIdAsync(model.Id);
                if (entity != null)
                    model.PassWord = entity.PassWord;
            }

            //调用父类方法
            return await base.UpdateAsync(model);
        }

        public override async Task<IResultModel> RemoveAsync(Guid id)
        {
            //初始化操作员禁止删除
            if (id == Guid.Parse("39F08CFD-8E0D-771B-A2F3-2639A62CA2FA"))
            {
                return ResultModel.Failed("初始化数据不能删除");
            }

            //调用父类方法
            return await base.RemoveAsync(id);
        }

        public async Task<IResultModel> UpdatePassword(UpdatePasswordDTO model)
        {
            //主键判断
            var entity = await _repository.Value.GetByIdAsync(model.AccountId);
            if (entity == null)
            {
                _logger.LogError($"error：entity AccountId {model.AccountId} does not exist");
                return ResultModel.NotExists;
            }

            //原密码验证
            if (!entity.PassWord.Equals($"{entity.UserName}_{model.OldPassword}".ToMd5Hash()))
            {
                return ResultModel.Failed("原密码错误", "OldPassword");
            }

            entity.PassWord = $"{entity.UserName}_{model.NewPassword}".ToMd5Hash();
            _repository.Value.Update(entity);

            if (await UnitOfWork.SaveChangesAsync() > 0)
            {
                return ResultModel.Success();
            }

            _logger.LogError($"error：UpdatePassword failed");
            return ResultModel.Failed("error：UpdatePassword failed", 500);
        }
    }
}