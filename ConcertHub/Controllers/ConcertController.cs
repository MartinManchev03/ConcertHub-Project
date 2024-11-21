using ConcertHub.Data;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConcertHub.Controllers
{
    public class ConcertController : Controller
    {
        private readonly ApplicationDbContext context;
        public ConcertController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IActionResult All()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            var categories = context.Categories.ToList();
            var model = new AddConcertViewModel();
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.Categories = categories;
            return View(model);
        }
    }
}
