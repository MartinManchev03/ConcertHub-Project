using ConcertHub.Data;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcertHub.Controllers
{
    public class ConcertController : Controller
    {
        private readonly ApplicationDbContext context;
        public ConcertController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> All()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await context.Categories.ToListAsync();
            var model = new AddConcertViewModel();
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.Categories = categories;
            return View(model);
        }
    }
}
