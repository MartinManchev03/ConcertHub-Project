using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConcertHub.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext context;
        public FeedbackController(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FeedBackViewModel model)
        {

            var feedback = new FeedBack
            {
                Comment = model.Comment,
                Rating = model.Rating,
                ConcertId = model.ConcertId,
                PostedById = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            await context.FeedBacks.AddAsync(feedback);
            await context.SaveChangesAsync();

            var feedbacks = await context.FeedBacks
            .Where(f => f.ConcertId == model.ConcertId)
            .Include(f => f.PostedBy)
            .Select(f => new AllFeedbacksViewModel()
            {
                Id = f.Id,
                PostedBy = f.PostedBy.UserName,
                ConcertOrganizer = f.Concert.Organizer.UserName,
                Comment = f.Comment,
                Rating = f.Rating
            })
            .ToListAsync();

            return PartialView("_FeedbackList", feedbacks);
        }

        [HttpGet]
        public async Task<IActionResult> All(Guid concertId)
        {
            var feedbacks = await context.FeedBacks
                .Where(f => f.ConcertId == concertId)
                .Include(f => f.PostedBy)
                .Select(f => new AllFeedbacksViewModel
                {
                    Id = f.Id,
                    PostedBy = f.PostedBy.UserName,
                    ConcertOrganizer = f.Concert.Organizer.UserName,
                    Comment = f.Comment,
                    Rating = f.Rating
                })
                .ToListAsync();

            return PartialView("_FeedbackList", feedbacks);
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            var feedback = await context.FeedBacks.FindAsync(id);
            context.FeedBacks.Remove(feedback);
            await context.SaveChangesAsync();

            var feedbacks = await context.FeedBacks
                .Where(f => f.ConcertId == feedback.ConcertId)
                .Include(f => f.PostedBy)
                .Select(f => new AllFeedbacksViewModel
                {
                    Id = f.Id,
                    PostedBy = f.PostedBy.UserName,
                    ConcertOrganizer = f.Concert.Organizer.UserName,
                    Comment = f.Comment,
                    Rating = f.Rating
                })
                .ToListAsync();

            return PartialView("_FeedbackList", feedbacks);
        }
    }
}
