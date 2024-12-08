using ConcertHub.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data.Interfaces
{
    public interface ITicketService
    {
        IPagedList<TicketsViewModel> GetAllTickets(int? page, string currentUserId);

        Task AddTicketsAsync(string concertEntry, Guid concertId, List<TicketsCheckBoxViewModel> tickets);

        Task EditTicketsAsync(string concertEntry, Guid concertId, List<TicketsCheckBoxViewModel> tickets);
    }
}
