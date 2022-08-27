using AllDto;
using ZookeeperBrowser.Code;
using System;
using System.Collections.Generic;
using WebApiClient;
using WebApiClient.Attributes;
using AllModel.MyOrm.Result;
using AllDto;

namespace ZookeeperBrowser.HttpApis
{
    [TokenFilter]
    [JsonReturn]
    public interface IAccountApi : IHttpApi
    {
        /// <summary>
        /// 根据ID获取指定账户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/Account/{id}")]
        ITask<ResultModel<AccountDTO>> QueryAsync(Guid id);

        /// <summary>
        /// 获取全部账户信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/Account")]
        ITask<ResultModel<List<AccountDTO>>> GetListAllAsync();

        /// <summary>
        /// 添加账户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/Account")]
        ITask<ResultModel<AccountDTO>> AddAsync([JsonContent]AccountDTO model);

        /// <summary>
        /// 修改账户信息
        /// </summary>
        /// <returns></returns>
        [HttpPut("api/Account")]
        ITask<ResultModel<AccountDTO>> UpdateAsync([JsonContent]AccountDTO model);

        /// <summary>
        /// 删除账户信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("api/Account/{id}")]
        ITask<ResultModel<string>> DeleteAsync(Guid id);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPut("api/Account/UpdatePassword")]
        ITask<ResultModel<string>> UpdatePasswordAsync(Guid id);
    }
}
