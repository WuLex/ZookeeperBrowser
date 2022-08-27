using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AllDto;
using AllDto.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AllModel.MyOrm.Result;

namespace CoreAPI.Controllers
{
    [Description("配置信息")]
    public class ConfigController : ControllerAbstract
    {
        private readonly Lazy<IConfigService> ConfigService;
        public ConfigController(ILogger<ControllerAbstract> logger, Lazy<IConfigService> configService) : base(logger)
        {
            ConfigService = configService;
        }

        [Description("通过Code，返回当前配置信息")]
        [OperationId("获取一条配置信息")]
        [Parameters(name="code", param = "配置代码")]
        [ResponseCache(Duration = 0)]
        [HttpGet("{code}")]
        public async Task<IResultModel> Query([Required]string code)
        {
            _logger.LogDebug($"获取一条配置信息,Code:{code}");
            return await ConfigService.Value.GetValue(code);
        }

        [Description("获取全部配置信息列表")]
        [ResponseCache(Duration = 0)]
        [HttpGet]
        public async Task<IResultModel> GetListAll()
        {
            _logger.LogDebug($"获取全部配置信息列表");
            return await ConfigService.Value.GetListAllAsync();
        }

        [Description("修改配置信息，成功后返回当前配置信息")]
        [OperationId("修改配置信息")]
        [HttpPut]
        public async Task<IResultModel> Update([FromBody]ConfigDTO model)
        {
            _logger.LogDebug("修改配置信息");
            return await ConfigService.Value.SetValue(model);
        }
    }
}
