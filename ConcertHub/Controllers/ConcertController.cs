using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

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
            var concerts = await context.Concerts
                .Where(c => c.IsDeleted == false)
                .Select(c => new AllConcertsViewModel()
                {
                    Id = c.Id,
                    Category = c.Category.Name,
                    ConcertName = c.ConcertName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Location = c.Location,
                    Organizer = c.Organizer.UserName
                })
                .ToListAsync();

            return View(concerts);
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
        [HttpPost]
        public async Task<IActionResult> Add(AddConcertViewModel model)
        {
			model.Categories = await context.Categories.ToListAsync();
            model.Tickets[2].IsChecked = true;
            if (!ModelState.IsValid)
            {
   
				return View(model);
			}
            var concert = new Concert()
            {
                ConcertName = model.ConcertName,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Location = model.Location,
                CategoryId = model.CategoryId,
                OrganizerId = GetCurrentUserId()
            };
            Console.WriteLine(model.Tickets[0].Name);
            Console.WriteLine(model.Tickets[0].IsChecked);
            Console.WriteLine(model.Tickets[1].Name);
            Console.WriteLine(model.Tickets[1].IsChecked);
            Console.WriteLine(model.Tickets[2].Name);
            Console.WriteLine(model.Tickets[2].IsChecked);
            await context.Concerts.AddAsync(concert);
            await context.SaveChangesAsync();
            return RedirectToAction("All");
        }


        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
