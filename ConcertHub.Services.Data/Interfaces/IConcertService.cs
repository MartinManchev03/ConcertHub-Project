using ConcertHub.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data.Interfaces
{
    public interface IConcertService
    {
        IPagedList<AllConcertsViewModel> GetAllConcerts(int? page, string userId);

        Task<ConcertViewModel> GetConcertForAddAsync();

        Task<Guid> AddConcertAsync(ConcertViewModel model, string creatorId);

        Task<ConcertViewModel> GetConcertForEditAsync(Guid concertId, string userId);

        Task EditConcertAsync(ConcertViewModel viewModel);

        Task<DeleteViewModel> GetConcertForDeleteAsync(Guid concertId, string userId);

        Task DeleteConcertAsync(Guid concertId);

        Task<ConcertDetailsViewModel> GetConcertDetailsAsync(Guid concertId);


    }
}
