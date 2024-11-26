using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConcertHub.Controllers
{
    public class PerformerController : Controller
    {
        private readonly ApplicationDbContext context;

        public PerformerController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> All()
        {
            var performers = await context.Performers
            .Select(p => new AllPerformersViewModel()
            {
                PerformerName = p.PerformerName,
                StageName = p.StageName,
                Id = p.Id,
                Creator = p.Creator.UserName
            })
            .ToListAsync();

            return View(performers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(PerformerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var model = new Performer()
            {
               PerformerName = viewModel.PerformerName,
               StageName = viewModel.StageName,
               Bio = viewModel.Bio,
               CreatorId = GetCurrentUserId()
            };
            await context.Performers.AddAsync(model);
            await context.SaveChangesAsync();
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await context.Performers
                .Select(p => new PerformerViewModel()
                {
                    Id = p.Id,
                    PerformerName = p.PerformerName,
                    Bio = p.Bio,
                    StageName = p.StageName
                })
                .FirstOrDefaultAsync(p => p.Id == id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PerformerViewModel viewModel)
        {
            var model = await context.Performers.FindAsync(viewModel.Id);
            model.PerformerName = viewModel.PerformerName;
            model.StageName = viewModel.StageName;
            model.Bio = viewModel.Bio;

            await context.SaveChangesAsync();
            return RedirectToAction("All");



        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await context.Performers
                .Select(p => new DetailsPerformerViewModel()
                {
                    Id = p.Id,
                    PerformerName = p.PerformerName,
                    Bio = p.Bio,
                    StageName = p.StageName,
                    Creator = p.Creator.UserName
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await context.Performers
                .Where(p =>  p.Id == id)
                .Select(p => new DeletePerformerViewModel()
                {
                    Id = p.Id,
                    PerformerName = p.PerformerName,
                    Creator = p.Creator.UserName
                })
                .FirstOrDefaultAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeletePerformerViewModel viewModel)
        {
            var model = await context.Performers.FindAsync(viewModel.Id);
            context.Performers.Remove(model);
            await context.SaveChangesAsync();
            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult Back()
        {
            return RedirectToAction("All");
        }
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
