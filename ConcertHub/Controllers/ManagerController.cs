using ConcertHub.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConcertHub.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        public IActionResult ManageConcerts()
        {
            return RedirectToAction("All", "Concert", new { page = 1});
        }

        public IActionResult ManagePerformers()
        {
            return RedirectToAction("All", "Performer", new { page = 1 });
        }
    }
}
