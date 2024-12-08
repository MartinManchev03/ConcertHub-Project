using ConcertHub.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data.Interfaces
{
    public interface IFeedbackService
    {
        Task AddFeedbackAsync(FeedBackViewModel model, string userId);
        Task<List<AllFeedbacksViewModel>> GetAllFeedbacksAsync(Guid concertId);
        Task<List<AllFeedbacksViewModel>> RemoveFeedbackAsync(Guid feedbackId);
    }
}
