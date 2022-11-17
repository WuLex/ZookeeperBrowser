using AllModel.MyOrm;
using System.Threading.Tasks;
using AllModel.MyOrm.Result;
using AllDto;
using AllModel;

namespace CoreAPI.Services.IService
{
    public interface IConfigService : IBaseService<ConfigEntity, ConfigDTO, int>
    {
        /// <summary>
        /// 获取配置脚本
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<IResultModel> GetValue(string code);

        /// <summary>
        /// 更新配置脚本
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> SetValue(ConfigDTO model);
    }
}