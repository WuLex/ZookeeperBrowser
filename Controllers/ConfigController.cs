using System;
using System.Linq;
using System.Threading.Tasks;
using ZookeeperBrowser.Common.Auth.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Student.DTO;
using ZookeeperBrowser.HttpApis;
using ZookeeperBrowser.ViewModels;
using yrjw.ORM.Chimp.Result;
using ZookeeperBrowser.Common.CommonToolsCore.Extensions;
using ZookeeperBrowser.Common.Auth.Jwt;
using ZookeeperBrowser.Common.CommonToolsCore.Helper;

namespace ZookeeperBrowser.Controllers
{
    public class ConfigController : Controller
    {
        private readonly ILogger<ConfigController> _logger;
        public readonly IConfigApi _configApi;

        public ConfigController(ILogger<ConfigController> logger, IConfigApi departApi)
        {
            _logger = logger;
            _configApi = departApi;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var model = new ConfigViewModel();
            var config = await _configApi.QueryAuthAsync();
            if (config.Success)
            {
                if (config.Data.Code.EqualsIgnoreCase("Auth"))
                {
                    model.AuthConfigData = config.Data.Value.ToJson<AuthConfigData>();
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAsync(ConfigViewModel model)
        {
            if (ModelState.IsValid)
            {
                var config = new ConfigDTO();
                config.Code = "Auth";
                config.Value = JsonHelper.SerializeJSON(model.AuthConfigData, true);
                IResultModel<ConfigDTO> result = await _configApi.UpdateAsync(config);
            }
            return View("Index", model);
        }
    }
}
