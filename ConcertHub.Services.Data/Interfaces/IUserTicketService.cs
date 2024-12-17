using ConcertHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data.Interfaces
{
    public interface IUserTicketService
    {
        Task<IEnumerable<MyTicketsViewModel>> GetMyTicketsAsync(string userId);
        IEnumerable<MyTicketsViewModel> GetMyTickets(string userId);

        Task BuyTicketAsync(string userId, Guid ticketId);

        Task RemoveTicketAsync(string userId, Guid ticketId);

        Task LeaveConcertAsync(string userId, Guid concertId);

        Task<bool> UpdateUserTicket(string userId, Guid concertId);

    }
}
