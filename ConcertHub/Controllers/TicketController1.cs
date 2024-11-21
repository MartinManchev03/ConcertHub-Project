using Microsoft.AspNetCore.Mvc;

namespace ConcertHub.Controllers
{
    public class TicketController1 : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
