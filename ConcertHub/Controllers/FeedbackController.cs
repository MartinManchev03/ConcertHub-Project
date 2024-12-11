using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> All(Guid concertId)
        {
            try
            {
                var feedbacks = await feedbackService.GetAllFeedbacksAsync(concertId);
                return PartialView("_FeedbackList", feedbacks);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FeedBackViewModel model)
        {
            var feedbacks = await this.feedbackService.AddFeedbackAsync(model, GetCurrentUserId());

            return PartialView("_FeedbackList", feedbacks);
        }

        [Authorize]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                var feedbacks = await feedbackService.RemoveFeedbackAsync(id, GetCurrentUserId());
                return PartialView("_FeedbackList", feedbacks);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message = ex.Message });
            }
        }
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
