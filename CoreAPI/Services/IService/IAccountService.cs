using System.Threading.Tasks;
using AllDto;
using System;
using AllModel;
using AllModel.MyOrm.Result;

namespace CoreAPI.Services.IService
{
    public interface IAccountService : IBaseService<AccountEntity, AccountDTO, Guid>
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> UpdatePassword(UpdatePasswordDTO model);
    }
}