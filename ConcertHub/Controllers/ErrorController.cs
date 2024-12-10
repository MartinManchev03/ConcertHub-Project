using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcertHub.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }
        public IActionResult Error(int statusCode, string message)
        {
            Response.StatusCode = statusCode;
            return View(message);
        }
    }
}
