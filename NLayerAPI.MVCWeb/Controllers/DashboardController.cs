using Microsoft.AspNetCore.Mvc;

namespace NLayerAPI.MVCWeb.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
