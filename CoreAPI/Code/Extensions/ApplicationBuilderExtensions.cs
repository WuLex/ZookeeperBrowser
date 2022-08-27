using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using CoreAPI.Code.Middleware;
using CoreAPI.Config;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AllModel.MyOrm.Result;
using AllDto.Common.CommonToolsCore.Helper;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 启用WebHost
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env">环境</param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebHost(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //异常处理
            app.UseExceptionHandle();

            //设置默认文档
            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);

            //启用默认页
            app.UseDefaultPage();

            //启动文档页
            app.UseDocs();

            //上传目录访问权限
            app.UseUpload();

            //CORS
            app.UseCors("Default");

            //反向代理
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //路由
            app.UseRouting();

            //认证
            app.UseAuthentication();

            //授权
            app.UseAuthorization();

            //配置端点
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //开启Swagger
            if (env.IsDevelopment())
            {
                app.UseCustomSwagger();
            }

            return app;
        }

        /// <summary>
        /// 启用默认页
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDefaultPage(this IApplicationBuilder app)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app");
            if (Directory.Exists(path))
            {
                var options = new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(path),
                    RequestPath = new PathString("/app")
                };

                app.UseStaticFiles(options);

                var rewriteOptions = new RewriteOptions().AddRedirect("^$", "app");

                app.UseRewriter(rewriteOptions);
            }

            return app;
        }

        /// <summary>
        /// 启动文档页
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDocs(this IApplicationBuilder app)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/api-docs");
            if (Directory.Exists(path))
            {
                var options = new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(path),
                    RequestPath = new PathString("/api-docs")
                };

                app.UseStaticFiles(options);
            }

            return app;
        }

        /// <summary>
        /// 上传目录访问权限
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUpload(this IApplicationBuilder app)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var options = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = new PathString("/Upload")
            };
            app.UseStaticFiles(options);
            return app;
        }

        /// <summary>
        /// 自定义Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api-docs";
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    version = version.Replace('_', '.');
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{BasicSetting.Setting.AssemblyName} {version}");
                });
                //c.SwaggerEndpoint("/swagger/v1.0/swagger.json", BasicSetting.Setting.AssemblyName + " v1.0");
                c.DisplayOperationId();
                c.DefaultModelExpandDepth(1); //模型示例部分中模型的默认扩展深度。
                c.DefaultModelsExpandDepth(1);//模型的默认扩展深度（设置为-1将完全隐藏模型）
                //c.DefaultModelRendering(ModelRendering.Model); //控制首次呈现API时如何显示模型。
                c.DisplayRequestDuration(); //控制Try-It-Out请求的请求持续时间（以毫秒为单位）的显示。
                c.DocExpansion(DocExpansion.None); //控制操作和标签的默认扩展设置。
                c.EnableFilter(); //顶部栏将显示一个编辑框，过滤显示
                //c.ShowExtensions();
            });
            return app;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandle(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandleMiddleware>();
            app.UseStatusCodePages(async context =>
            {
                if(context.HttpContext.Response.StatusCode != 200)
                {
                    
                    if (context.HttpContext.Request.Path.Value.ToLower().StartsWith("/upload/"))
                    {
                        context.HttpContext.Response.Redirect("/Upload/upload-404.png");
                    }
                    else
                    {
                        context.HttpContext.Response.ContentType = "application/json";
                        await context.HttpContext.Response.WriteAsync(
                            JsonHelper.SerializeJSON(ResultModel.Failed($"Status code page, status code: {context.HttpContext.Response.StatusCode}"))
                            );
                    }
                    
                }
            });
            return app;
        }
    }
}
