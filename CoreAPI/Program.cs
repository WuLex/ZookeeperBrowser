
using CoreAPI.Config;

#region 绑定配置类
IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
configuration.GetSection("Setting").Bind(BasicSetting.Setting);
if (BasicSetting.Setting.Urls.IsNull())
    BasicSetting.Setting.Urls = "http://*:5000";

#endregion


//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder(args);

//WebApplicationBuilder 主要负责 4 项工作：
//使用 builder.Configuration 添加配置。
//使用 builder.Services 添加服务
//使用 builder.Logging 配置日志
//配置 IHostBuilder 和 IWebHostBuilder



#region 配置 IHostBuilder 和 IWebHostBuilder

#endregion

#region Startup可选
//try
//{
//    builder.WebHost.UseStartup<Startup>();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//    throw;
//} 
#endregion

#region 添加服务
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddWebHost(builder.Environment);
#endregion

#region 改变端口方式一
//builder.WebHost.UseUrls("http://localhost:3045");
#endregion

var app = builder.Build();
 
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseWebHost(app.Environment);

//app.UseAuthorization();
//app.MapControllers();

#region 改变端口方式二
//app.Run("http://localhost:6054");
#endregion

app.Run();
