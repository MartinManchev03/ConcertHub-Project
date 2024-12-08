using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConcertHub.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FeedBackViewModel model)
        {
            var feedbacks = await this.feedbackService.AddFeedbackAsync(model, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return PartialView("_FeedbackList", feedbacks);
        }

        [HttpGet]
        public async Task<IActionResult> All(Guid concertId)
        {

            var feedbacks = await feedbackService.GetAllFeedbacksAsync(concertId);
            return PartialView("_FeedbackList", feedbacks);
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            var feedbacks = await feedbackService.RemoveFeedbackAsync(id);

            return PartialView("_FeedbackList", feedbacks);
        }
    }
}
