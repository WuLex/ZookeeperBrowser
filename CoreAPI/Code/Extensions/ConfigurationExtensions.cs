using AllDto.Common.CommonToolsCore.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 配置项绑定
        /// </summary>
        /// <typeparam name="T">配置项模型</typeparam>
        /// <param name="configuration"></param>
        /// <param name="key">key与配置项模型中静态属性名称要一致</param>
        /// <returns></returns>
        public static IConfiguration Binding<T>(this IConfiguration configuration, string key) where T : class
        {
            var pro = typeof(T).GetProperty(key);
            if (pro != null)
            {
                pro.SetValue(pro, configuration.GetSection(key).Get<T>());
            }
            else
            {
                Console.WriteLine($"To: error Binding<T> T={typeof(T).Name} key={key}");
            }

            return configuration;
        }

        /// <summary>
        /// 热更新，运行时更改配置文件自动更新模型绑定值
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="pms">配置模型中静态属性对象，多个使用逗号分隔</param>
        public static void OnChange(this IConfiguration configuration, params Object[] pms)
        {
            //配置更改时重新绑定
            ChangeToken.OnChange(() => configuration.GetReloadToken(), () =>
            {
                foreach (object item in pms)
                {
                    var pro = item.GetType().GetProperties().FirstOrDefault(p => p.SetMethod.IsStatic);
                    if (pro != null)
                    {
                        pro.SetValue(pro, configuration.GetSection(pro.Name).Get(item.GetType()));
                        Console.WriteLine($"To:{JsonHelper.SerializeJSON(pro.GetValue(pro.Name))}");
                    }
                }
            });
        }
    }
}