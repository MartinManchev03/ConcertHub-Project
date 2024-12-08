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
        Task<AddConcertPerformersViewModel> GetAllConcertPerformersAsync(Guid concertId);
        Task AddConcertPerformerAsync(AddConcertPerformersViewModel viewModel);
        Task<ConcertPerformersViewModel> RemoveConcertPerformerAsync(Guid performerId, Guid concertId);
    }
}
