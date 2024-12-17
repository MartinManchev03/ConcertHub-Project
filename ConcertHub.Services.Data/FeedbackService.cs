using ConcertHub.Data.Configuration;
using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<FeedBack, Guid> feedbackRepository;
        private readonly IRepository<Concert, Guid> concertRepository;
        private readonly UserManager<IdentityUser> userManager;

        public FeedbackService(IRepository<FeedBack, Guid> feedbackRepository, IRepository<Concert, Guid> concertRepository, UserManager<IdentityUser> userManager)
        {
            this.feedbackRepository = feedbackRepository;
            this.concertRepository = concertRepository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AllFeedbacksViewModel>> GetAllFeedbacksAsync(Guid concertId)
        {
            if(await concertRepository.GetByIdAsync(concertId) == null)
            {
                 throw new ArgumentException("Error 404");
            }
            var feedbacks = await this.feedbackRepository
                .GetAllAttached()
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

            return feedbacks;
        }
        //Adding same services only without tolistasync so the test could pass i get error.
        public async Task<IEnumerable<AllFeedbacksViewModel>> GetAllFeedbacks(Guid concertId)
        {
            if (await concertRepository.GetByIdAsync(concertId) == null)
            {
                throw new ArgumentException("Error 404");
            }
            var feedbacks = this.feedbackRepository
                .GetAllAttached()
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
                .ToList();

            return feedbacks;
        }

        public async Task<IEnumerable<AllFeedbacksViewModel>> AddFeedbackAsync(FeedBackViewModel model, string userId)
        {
            var feedback = new FeedBack
            {
                Comment = model.Comment,
                Rating = model.Rating,
                ConcertId = model.ConcertId,
                PostedById = userId
            };
            await this.feedbackRepository.AddAsync(feedback);

            var feedbacks = await this.feedbackRepository
                .GetAllAttached()
                .Where(f => f.ConcertId == model.ConcertId)
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

            return feedbacks;
        }
        //adding this and other services similar like this to pass the test becuase of ienumasync and iquerable
        public async Task<IEnumerable<AllFeedbacksViewModel>> AddFeedback(FeedBackViewModel model, string userId)
        {
            var feedback = new FeedBack
            {
                Comment = model.Comment,
                Rating = model.Rating,
                ConcertId = model.ConcertId,
                PostedById = userId
            };
            await this.feedbackRepository.AddAsync(feedback);

            var feedbacks = this.feedbackRepository
                .GetAllAttached()
                .Where(f => f.ConcertId == model.ConcertId)
                .Include(f => f.PostedBy)
                .Select(f => new AllFeedbacksViewModel
                {
                    Id = f.Id,
                    PostedBy = f.PostedBy.UserName,
                    ConcertOrganizer = f.Concert.Organizer.UserName,
                    Comment = f.Comment,
                    Rating = f.Rating
                })
                .ToList();

            return feedbacks;
        }


        public async Task<IEnumerable<AllFeedbacksViewModel>> RemoveFeedbackAsync(Guid feedbackId, string userId)
        {
            var feedback = await feedbackRepository.GetAllAttached().Include(f => f.Concert).FirstOrDefaultAsync(f => f.Id == feedbackId);

            if (feedback == null)
            {
                throw new ArgumentException("Error 404");
            }
            if (!await IsAuthorizedToPerformAction(feedback.Concert.OrganizerId, userId))
            {
                throw new ArgumentException("Error 403");
            }
            await feedbackRepository.DeleteAsync(feedbackId);
            var feedbacks = await GetAllFeedbacksAsync(feedback.ConcertId);

            return feedbacks;
        }
        private async Task<bool> IsAuthorizedToPerformAction(string organizerId, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (organizerId != userId && !await userManager.IsInRoleAsync(user, "Manager") && !await userManager.IsInRoleAsync(user, "Admin"))
            {
                return false;
            }
            return true;
        }
    }
}
