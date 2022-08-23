using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// 设置自定义配置
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="filename">配置文件名称，使用约定配置文件放在项目的Config目录中</param>
        /// <param name="optional">是否忽略检查文件，true忽略</param>
        /// <param name="reloadOnChange">支持热更新</param>
        /// <returns></returns>
        public static IHostBuilder Configure(this IHostBuilder hostBuilder, string filename, bool optional = false, bool reloadOnChange = false)
        {
            return hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                var filePath = Path.Combine(AppContext.BaseDirectory, "Config");
                //config.SetBasePath(filePath);
                config.AddJsonFile(filePath + $"/{filename.ToLower()}.json", optional: optional, reloadOnChange: reloadOnChange);
                if (env.IsDevelopment())
                {
                    config.AddJsonFile(filePath + $"/{filename.ToLower()}.{env.EnvironmentName}.json", optional: true, reloadOnChange: reloadOnChange);
                }
            });
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="hostBuilder"></param>
        public static void Run(this IHostBuilder hostBuilder)
        {
            hostBuilder.Build().Run();
        }
    }
}
