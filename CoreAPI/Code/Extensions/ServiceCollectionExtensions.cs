using AutoMapper;
using AllDto.Common.Cache.MemoryCache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CoreAPI.Code.Filters;
using CoreAPI.Code.Middleware;
using CoreAPI.Code.WebApi;
using CoreAPI.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApiClient;
using AllDto.Profiles;
using AllDto.Common.CommonToolsCore.Extensions;
using AllDto.Common.Auth.Jwt;
using AllModel.Code;
using AllModel.MyOrm;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加WebHost
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env">环境</param>
        /// <returns></returns>
        public static IServiceCollection AddWebHost(this IServiceCollection services, IWebHostEnvironment env)
        {
            //将控制器的寄宿器转为注册的服务
            services.AddControllers().AddControllersAsServices().AddNewtonsoftJson(options =>
            {
                //设置日期格式化
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            services.AddOptions();

            //添加所有通过特性注入的服务
            services.AddNetModularServices();

            //服务端缓存
            services.AddResponseCaching();

            //添加缓存
            services.AddCache();

            //添加CORS
            services.AddCors(BasicSetting.Setting);

            //添加ORM
            services.AddORM(BasicSetting.Setting);

            //注册懒加载，与Autofac冲突，使用懒加载禁用Autofac注入方式。
            services.AddTransient(typeof(Lazy<>));

            //添加AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            //添加Swagger
            if (env.IsDevelopment())
            {
                services.AddSwagger();
            }

            //Jwt身份认证
            services.AddJwtAuth();

            //开启模型验证结果格式化
            services.AddValidators();

            //添加HttpClient相关
            services.AddSingleton<IHttpApiFactory<IWebApiHelper>, HttpApiFactory<IWebApiHelper>>(p =>
            {
                return new HttpApiFactory<IWebApiHelper>().ConfigureHttpApiConfig(c =>
                {
                    // Api 地址
                    c.HttpHost = new Uri(BasicSetting.Setting.ApiUrl);
                });
            });
            services.AddTransient(p =>
            {
                var factory = p.GetRequiredService<IHttpApiFactory<IWebApiHelper>>();
                return factory.CreateHttpApi();
            });

            //解决Multipart body length limit 134217728 exceeded
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });

            return services;
        }

        /// <summary>
        /// 注册Swagger生成器,可以定义一个或多个 Swagger 文档
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    version = version.Replace('_', '.');
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = $"{BasicSetting.Setting.AssemblyName} 接口文档 - Net6",
                        Description = $"{BasicSetting.Setting.AssemblyName} HTTP API " + version,
                        Contact = new OpenApiContact { Name = "灵风幻火", Email = "**********@qq.com", Url = new Uri("https://www.cnblogs.com/Wulex/") },
                        //License = new OpenApiLicense { Name = BasicSetting.Setting.AssemblyName + " 官方文档", Url = new Uri("https://www.cnblogs.com/Wulex/") }
                    });
                    c.OrderActionsBy(o => o.RelativePath);
                });
                
                //为 Swagger JSON and UI设置xml文档注释路径
                var filePath = Path.Combine(System.AppContext.BaseDirectory, BasicSetting.Setting.AssemblyName + ".xml"); //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                c.IncludeXmlComments(filePath);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT认证请求头格式: \"Token: {token}\"",
                    Name = "Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                };

                //添加设置Token的按钮
                c.AddSecurityDefinition("Bearer", securityScheme);

                //添加Jwt验证设置
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                //链接转小写过滤器
                c.DocumentFilter<LowercaseDocumentFilter>();

                //描述信息处理
                c.DocumentFilter<DescriptionDocumentFilter>();

                //隐藏属性
                c.SchemaFilter<IgnorePropertySchemaFilter>();

                //表单模型处理
                c.RequestBodyFilter<IgnorePropertyRequestBodyFilter>();
            });
            //支持newstonsoftjson
            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        /// <summary>
        /// 添加CORS允许跨域访问
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IServiceCollection AddCors(this IServiceCollection services, BasicSetting setting)
        {
            if (setting.WithOrigins != null && setting.WithOrigins.Length > 0)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("AnotherPolicy",
                        builder =>
                        {
                            builder.WithOrigins(setting.WithOrigins)
                            .SetPreflightMaxAge(new TimeSpan(0, 0, 180))
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        });
                });
            }
            else
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("Default",
                        builder => builder.AllowAnyOrigin()
                            .SetPreflightMaxAge(new TimeSpan(0, 0, 180))
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });
            }
            return services;
        }

        /// <summary>
        /// 添加ORM
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IServiceCollection AddORM(this IServiceCollection services, BasicSetting setting)
        {
            if (setting.DbType == DbType.MYSQL)
            {
                //UseLazyLoadingProxies 通过延迟加载获取导航属性数据
                services.AddChimp<myDbContext>(opt => opt.UseLazyLoadingProxies().UseMySql(setting.ConnectionString, new MySqlServerVersion(new Version(8, 0, 29)),
                    b => b.MigrationsAssembly(setting.AssemblyName)));
            }
            else
            {
                services.AddChimp<myDbContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(setting.ConnectionString,
                    b => b.MigrationsAssembly(setting.AssemblyName)));
            }
            return services;
        }

        /// <summary>
        /// 开启模型验证结果格式化
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.TryAddSingleton<IValidateResultFormatHandler, ValidateResultFormatHandler>();
            return services;
        }
    }
}
