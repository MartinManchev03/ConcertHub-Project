using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConcertHub.Controllers
{
    public class ConcertPerformerController : Controller
    {
        private readonly ApplicationDbContext context;

        public ConcertPerformerController(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        [HttpGet]
        public IActionResult Add(Guid concertId)
        {
            var concertPerformers = new AddConcertPerformersViewModel()
            {
                ConcertId = concertId
            };

            var alreadyAssociatedPerformerIds = context.ConcertsPerformers
                .Where(cp => cp.ConcertId == concertId)
                .Select(cp => cp.PerformerId)
                .ToList();

            foreach (var p in context.Performers)
            {
                if (!alreadyAssociatedPerformerIds.Contains(p.Id))
                {
                    concertPerformers.ConcertPerformers.Add(new ConcertPerformersCheckboxViewModel()
                    {
                        PerformerId = p.Id,
                        PerformerName = p.PerformerName
                    });
                }
            }
            return View(concertPerformers);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddConcertPerformersViewModel viewModel)
        {
            foreach(var p in viewModel.ConcertPerformers)
            {
                if (p.IsChecked)
                {
                    var cp = new ConcertPerformer()
                    {
                        PerformerId = p.PerformerId,
                        ConcertId = viewModel.ConcertId
                    };
                    await context.ConcertsPerformers.AddAsync(cp);
                }
            }
            await context.SaveChangesAsync();
            return RedirectToAction("Details", "Concert", new {id = viewModel.ConcertId});
        }

        public async Task<IActionResult> Remove(Guid performerId, Guid concertId)
        {
            var concertPerformer = await context.ConcertsPerformers
                .Where(cp => cp.ConcertId == concertId && cp.PerformerId == performerId)
                .Include(c => c.Concert)
                .Include(c => c.Concert.Organizer)
                .FirstOrDefaultAsync();
            context.Remove(concertPerformer);
            await context.SaveChangesAsync();

            var concertPerformers = new ConcertPerformersViewModel()
            {
                ConcertId = concertId,
                Organizer = concertPerformer.Concert.Organizer.UserName,
                PerformersNames = context.ConcertsPerformers.Where(cp => cp.ConcertId == concertId)
                    .Select(cp => new PerformerConcertViewModel()
                    {
                        PerformerId = cp.PerformerId,
                        PerformerName = cp.Performer.PerformerName
                    })
                    .ToList()
            };

            return PartialView("_AllConcertPerformers", concertPerformers);
        }
    }
}
