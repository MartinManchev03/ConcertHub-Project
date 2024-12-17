using ConcertHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data.Interfaces
{
    public interface IConcertPerformerService
    {
        Task<AddConcertPerformersViewModel> GetAllConcertPerformersAsync(Guid concertId, string userId);
        Task<AddConcertPerformersViewModel> GetAllConcertPerformers(Guid concertId, string userId);
        Task AddConcertPerformerAsync(AddConcertPerformersViewModel viewModel);
        Task<ConcertPerformersViewModel> RemoveConcertPerformerAsync(Guid performerId, Guid concertId, string userId);
        Task<ConcertPerformersViewModel> RemoveConcertPerformer(Guid performerId, Guid concertId, string userId);
    }
}
