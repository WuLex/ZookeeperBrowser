using CoreAPI.Code.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CoreAPI.Controllers
{
    /// <summary>
    /// 控制器抽象类
    /// </summary>
    [Route("api/[controller]")]
    [PermissionValidate]
    [ValidateResultFormat]
    [ApiController]
    public abstract class ControllerAbstract : ControllerBase
    {
        protected readonly ILogger<ControllerAbstract> _logger;
        public ControllerAbstract(ILogger<ControllerAbstract> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected IActionResult ExportExcel(string filePath, string fileName)
        {
            if (fileName.IsNull())
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            return PhysicalFile(filePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", HttpUtility.UrlEncode(fileName), true);
        }
    }
}
