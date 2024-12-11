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
        Task<IEnumerable<AllFeedbacksViewModel>> AddFeedbackAsync(FeedBackViewModel model, string userId);
        Task<IEnumerable<AllFeedbacksViewModel>> GetAllFeedbacksAsync(Guid concertId);
        Task<IEnumerable<AllFeedbacksViewModel>> RemoveFeedbackAsync(Guid feedbackId, string userId);
    }
}
