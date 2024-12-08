using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data
{
    public class UserTicketService : IUserTicketService
    {
        private readonly IMappingRepository<UserTicket, string, Guid> userTicketRepository;

        public UserTicketService(IMappingRepository<UserTicket, string, Guid> userTicketRepository)
        {
            this.userTicketRepository = userTicketRepository;
        }

        public async Task BuyTicketAsync(string userId, Guid ticketId)
        {
            var userTickets = await userTicketRepository
                .GetAllAttached()
                .Where(ut => ut.TicketId == ticketId)
                .Where(ut => ut.UserId == userId)
                .FirstOrDefaultAsync();

            if (userTickets == null)
            {
                var userTicket = new UserTicket()
                {
                    UserId = userId,
                    TicketId = ticketId
                };
                await userTicketRepository.AddAsync(userTicket);
            }
        }

        public async Task<IEnumerable<MyTicketsViewModel>> GetMyTicketsAsync(string userId)
        {
            var usersTickets = await userTicketRepository
                                    .GetAllAttached()
                                    .Where(ut => ut.UserId == userId)
                                    .Select(ut => new MyTicketsViewModel()
                                    {
                                        Id = ut.Ticket.Id,
                                        ConcertName = ut.Ticket.Concert.ConcertName,
                                        TicketType = ut.Ticket.TicketType,
                                        IsUsed = ut.IsUsed
                                    })
                                    .ToListAsync();
            return usersTickets;
        }

        public async Task LeaveConcertAsync(string userId, Guid concertId)
        {
            var userTicket = await userTicketRepository.GetAllAttached().Where(ut => ut.Ticket.ConcertId == concertId && ut.UserId == userId).FirstOrDefaultAsync();
            await userTicketRepository.DeleteAsync(userId, userTicket.TicketId);
        }

        public async Task RemoveTicketAsync(string userId, Guid ticketId)
        {
            await userTicketRepository.DeleteAsync(userId, ticketId);
        }

        public async Task<bool> UpdateUserTicket(string userId, Guid concertId)
        {
            var userTicket = await userTicketRepository.GetAllAttached()
            .Where(ut => ut.Ticket.ConcertId == concertId && ut.UserId == userId)
            .FirstOrDefaultAsync();

            if (userTicket == null)
            {
                return false;
            }
            userTicket.IsUsed = true;
            await userTicketRepository.UpdateAsync(userTicket);
            return true;
        }
    }
}
