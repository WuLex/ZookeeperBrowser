using Student.DTO;
using ZookeeperBrowser.Code;
using System.Collections.Generic;
using WebApiClient;
using WebApiClient.Attributes;
using yrjw.ORM.Chimp.Result;

namespace ZookeeperBrowser.HttpApis
{
    [TokenFilter]
    [JsonReturn]
    public interface IDepartApi : IHttpApi
    {
        /// <summary>
        /// 根据ID获取指定部门
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/Depart/{id}")]
        ITask<ResultModel<DepartDTO>> QueryAsync(int id);

        /// <summary>
        /// 获取全部部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/Depart")]
        ITask<ResultModel<List<DepartDTO>>> GetListAllAsync();

        /// <summary>
        /// 获取所有班级列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/Depart/GetClassesList")]
        ITask<ResultModel<List<DepartDTO>>> GetClassesListAsync();

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/Depart")]
        ITask<ResultModel<DepartDTO>> AddAsync([JsonContent]DepartDTO model);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <returns></returns>
        [HttpPut("api/Depart")]
        ITask<ResultModel<DepartDTO>> UpdateAsync([JsonContent]DepartDTO model);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <returns></returns>
        [HttpDelete("api/Depart/{id}")]
        ITask<ResultModel<string>> DeleteAsync(int id);
    }
}
