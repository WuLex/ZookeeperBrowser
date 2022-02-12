using Microsoft.OpenApi.Models;
using WebApiClient;
using ZookeeperBrowser.Common.CommonToolsCore.Extensions;
using ZookeeperBrowser.Services;
using ZookeeperBrowser.Settings;
using ZookeeperBrowser.Utils;


#region 获取配置信息
var configuration = new ConfigurationBuilder()
               .SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
configuration.GetSection("Setting").Bind(AppSetting.Setting);

if (AppSetting.Setting.Urls.IsNull())
{
    AppSetting.Setting.Urls = "http://*:8080";
}
#endregion



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
//使用Session
builder.Services.AddSession();

builder.Services.AddScoped<IZookeeperService, ZookeeperService>();
//builder.Services.AddScoped<IMyDependency2, MyDependency2>();
builder.Services.AddSingleton<ZooKeeperManager>(ZooKeeperManager.Instance);

//添加HttpClient相关
var types = typeof(Program).Assembly.GetTypes()
            .Where(type => type.IsInterface
            && ((System.Reflection.TypeInfo)type).ImplementedInterfaces != null
            && type.GetInterfaces().Any(a => a.FullName == typeof(IHttpApi).FullName));
foreach (var type in types)
{
    builder.Services.AddHttpApi(type);
    builder.Services.ConfigureHttpApi(type, o => {
        o.HttpHost = new Uri(AppSetting.Setting.ApiUrl);
    });
}

#region 添加swagger注释

var basePath = AppContext.BaseDirectory;

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ZooKeeper.Admin.Api"
    });

    #region 暂未使用

    //var xmlPath = Path.Combine(basePath, "NET6.Api.xml");
    //c.IncludeXmlComments(xmlPath, true);
    //var xmlDomainPath = Path.Combine(basePath, "NET6.Domain.xml");
    //c.IncludeXmlComments(xmlDomainPath, true);
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Description = "Value: Bearer {token}",
    //    Name = "Authorization",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer"
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    //{
    //  {
    //    new OpenApiSecurityScheme
    //    {
    //      Reference = new OpenApiReference
    //      {
    //        Type = ReferenceType.SecurityScheme,
    //        Id = "Bearer"
    //      },Scheme = "oauth2",Name = "Bearer",In = ParameterLocation.Header,
    //    },new List<string>()
    //  }
    //});

    #endregion 暂未使用
});

#endregion 添加swagger注释

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseCors(p =>
{
    p.AllowAnyOrigin();
    p.WithMethods("GET");
    p.AllowAnyHeader();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#region 启用swaggerUI

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZooKeeper.Admin.Api V1Docs");
    c.RoutePrefix = string.Empty;
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
});

#endregion 启用swaggerUI

app.Run();