using Microsoft.AspNetCore.Mvc;

namespace ZookeeperBrowser.Controllers
{
    public class LayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}