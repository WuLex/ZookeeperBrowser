using yrjw.ORM.Chimp;
using System.Threading.Tasks;
using yrjw.ORM.Chimp.Result;
using AllDto;
using AllModel;

namespace AllDto.Services
{
    public interface IConfigService : IBaseService<Config, ConfigDTO, int>
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
