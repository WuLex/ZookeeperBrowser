using AllDto.Common.Auth.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using ZookeeperBrowser.Models.Commands;
using ZookeeperBrowser.Services;

namespace ZookeeperBrowser.Controllers
{
    [Authorize]
    public class CommandsController : Controller
    {
        public readonly IZookeeperService _zookeeperService;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public CommandsController(IZookeeperService zookeeperService, IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _config = configuration;
            _zookeeperService = zookeeperService;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClientFactory.CreateClient("myHttpClient");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.AccountName = _httpContextAccessor.HttpContext.User.FindFirst(nameof(ClaimsName.AccountName)).Value;
            return View();
        }

        //[HttpGet]
        [HttpGet("/Commands/monitor")]
        public async Task<IActionResult> GetMonitorAsync()
        {
            //byte[] data = zkClient.GetData("/commands/monitor");
            //string result = System.Text.Encoding.UTF8.GetString(data);
            //MonitorData monitorData = JsonConvert.DeserializeObject<MonitorData>(result);
            //return Ok(monitorData);
           
            // 设置请求的URL和Headers
            //var requestUrl = "http://localhost:8080/commands/monitor";
            var requestUrl = "/commands/monitor";
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                // 发送请求并获取响应
                var response = await _httpClient.GetAsync(requestUrl);

                // 如果响应成功，则将其转换为MonitorCommandResult实例
                if (response.IsSuccessStatusCode)
                {
                    //response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var monitorResult = JsonConvert.DeserializeObject<MonitorCommandResult>(json);


                    // 打印结果
                    Console.WriteLine($"ZooKeeper监控数据：\n{JsonConvert.SerializeObject(monitorResult, Formatting.Indented)}");
                    return Ok(monitorResult);
                }
                else
                {
                    Console.WriteLine($"请求失败：{response.StatusCode} - {response.ReasonPhrase}");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"请求失败：{ex.Message}");
                return BadRequest();
            }
        }

        [HttpGet("/Commands/configuration")]
        public async Task<IActionResult> GetConfiguration()
        {
            var requestUrl = "/commands/configuration";
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            try
            {
                // 发送请求并获取响应
                var response = await _httpClient.GetAsync(requestUrl);
                // 如果响应成功，则将其转换为ZooKeeperConfiguration实例
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var configResult = JsonConvert.DeserializeObject<ZooKeeperConfiguration>(json);
                    return Ok(configResult);
                }
                else
                {
                    Console.WriteLine($"请求失败：{response.StatusCode} - {response.ReasonPhrase}");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"请求失败：{ex.Message}");
                return BadRequest();
            }
        }
      
        [HttpGet("/Commands/stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var requestUrl = "/commands/stats";
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            try
            {
                // 发送请求并获取响应
                var response = await _httpClient.GetAsync(requestUrl);
                // 如果响应成功，则将其转换为ZookeeperStats实例
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var configResult = JsonConvert.DeserializeObject<ZookeeperStats>(json);
                    return Ok(configResult);
                }
                else
                {
                    Console.WriteLine($"请求失败：{response.StatusCode} - {response.ReasonPhrase}");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"请求失败：{ex.Message}");
                return BadRequest();
            }
        }
        //[HttpGet("server_stats")]
        //public IActionResult GetServerStats()
        //{
        //    byte[] data = zkClient.GetData("/commands/server_stats");
        //    string result = System.Text.Encoding.UTF8.GetString(data);
        //    ServerStatsData serverStatsData = JsonConvert.DeserializeObject<ServerStatsData>(result);
        //    return Ok(serverStatsData);
        //}

    }
}