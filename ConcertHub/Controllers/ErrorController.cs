using ConcertHub.ViewModels;
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
        public IActionResult Error(string message)
        {
            var model = new ErrorViewModel();
            if(message == "Error 404")
            {
                Response.StatusCode = 404;
                model.StatusCodeTitle = "Error 404 - Page Not Found";
                model.ErrorMessage = "The page you are looking for does not exist.";


            }
            else if(message == "Error 403")
            {
                Response.StatusCode = 403;
                model.StatusCodeTitle = "Error 403 - Forbidden";
                model.ErrorMessage = "You do not have permission to access this resource. Please check your credentials or contact your administrator if you believe this is an error.";
            }
            else
            {
                Response.StatusCode = 500;
                model.StatusCodeTitle = "Error 500 - Internal Server Error";
                model.ErrorMessage = "An unexpected error occurred. Please try again later.";
            }
            return View(model);
        }
    }
}
