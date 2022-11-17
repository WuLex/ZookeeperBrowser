using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace AllModel.MyOrm
{
    /// <summary>
    /// 已弃用，Autofac注入方式，无法在ConfigureServices中获取服务，ConfigureContainer后触发导致，用户无法在扩展IServiceCollection时通过服务访问数据库
    /// 官方DI注入与Autofac混用可解决以上问题，但服务接口中使用Lazy<T>，注册services.AddTransient(typeof(Lazy<>))后，Autofac报异常。
    /// </summary>
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = ReflectionHelper.GetAllAssembliesCoreWeb();

            //注册Service和Controller
            builder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Service") |
                    t.HasImplementedRawGeneric(typeof(IDependency)) && t.IsClass)
                .PublicOnly().AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Controller")).PropertiesAutowired();
        }
    }
}