using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConcertHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
