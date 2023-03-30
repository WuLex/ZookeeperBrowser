using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static org.apache.zookeeper.ZooDefs;
using System.Diagnostics;
using System.Text;
using ZookeeperBrowser.Models;
using System.Net.Sockets;
using ZookeeperBrowser.Services;
using org.apache.zookeeper;
using org.apache.zookeeper.data;

namespace ZookeeperBrowser.Controllers
{
    public class ZooKeeperMonitorController : Controller
    {
        public readonly IZookeeperService _zookeeperService;
        private readonly ILogger<ZooKeeperMonitorController> _logger;

        public ZooKeeperMonitorController(IZookeeperService zookeeperService, ILogger<ZooKeeperMonitorController> logger)
        {
            _zookeeperService = zookeeperService;
            _logger = logger;
        }


        //public async IActionResult Monitor()
        //{
        //    // Define the command to execute
        //    var command = "echo mntr | nc localhost 2181"; // Replace with your command and port number

        //    // Create a child node for output monitoring
        //    var outputNode =await _zookeeperService.CreateAsync("/output", null, Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);

        //    // Execute the command and redirect the output to the output node
        //    var process = new Process();
        //    process.StartInfo.FileName = "cmd.exe";
        //    process.StartInfo.Arguments = $"/c {command} > {outputNode}";
        //    process.StartInfo.UseShellExecute = false;
        //    process.StartInfo.RedirectStandardOutput = true;
        //    process.Start();

        //    // Read the output from the output node and parse it into metrics
        //    var output =await _zookeeperService.GetDataAsync("/output");
        //    //var outputString = Encoding.UTF8.GetString(output);
        //    var metrics = JsonConvert.DeserializeObject<ZookeeperMetrics>(output);

        //    // Pass the metrics to the view
        //    return View("Monitor", metrics);
        //}

        public async Task<IActionResult> Index()
        {
            var zkMetrics = await GetZooKeeperMetricsAsync("localhost", 2181);

            return View(zkMetrics);
        }

        private async Task<ZookeeperMetrics> GetZooKeeperMetricsAsync(string host, int port)
        {
            ZookeeperMetrics zkMetrics = null;

            try
            {
                using (var client = new TcpClient(host, port))
                {
                    using (var stream = client.GetStream())
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            await writer.WriteLineAsync("mntr");
                            await writer.FlushAsync();

                            using (var reader = new StreamReader(stream))
                            {
                                var response = await reader.ReadToEndAsync();
                                zkMetrics = JsonConvert.DeserializeObject<ZookeeperMetrics>(response);
                            }
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                _logger.LogError(ex, "Failed to connect to ZooKeeper at {host}:{port}", host, port);
            }

            return zkMetrics;
        }
    }
}
