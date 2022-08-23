using Microsoft.AspNetCore.Mvc;
using org.apache.zookeeper;
using System.Text;
using ZookeeperBrowser.Message;
using ZookeeperBrowser.Message.Request;
using ZookeeperBrowser.Message.Response;
using ZookeeperBrowser.Services;
using ZookeeperBrowser.Utils;

namespace ZookeeperBrowser.Controllers
{
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
      
        public AdminController()
        {
            
        }

    }
}