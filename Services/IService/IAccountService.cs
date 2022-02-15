using yrjw.ORM.Chimp;
using System.Threading.Tasks;
using yrjw.ORM.Chimp.Result;
using Student.DTO;
using System;
using ZookeeperBrowser.Model;

namespace ZookeeperBrowser.Services.IService
{
    public interface IAccountService : IBaseService<Account, AccountDTO, Guid>
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> UpdatePassword(UpdatePasswordDTO model);
    }
}
