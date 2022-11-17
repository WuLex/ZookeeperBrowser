using AllDto;
using ZookeeperBrowser.Code;
using WebApiClient;
using WebApiClient.Attributes;
using AllModel.MyOrm.Result;

namespace ZookeeperBrowser.HttpApis
{
    [TokenFilter]
    [JsonReturn]
    public interface IConfigApi : IHttpApi
    {
        /// <summary>
        /// 获取权限配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/Config/Auth")]
        ITask<ResultModel<ConfigDTO>> QueryAuthAsync();

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("api/Config")]
        ITask<ResultModel<ConfigDTO>> UpdateAsync([JsonContent] ConfigDTO model);
    }
}