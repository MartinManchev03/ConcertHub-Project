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
        private readonly IRepository<Ticket, Guid> ticketRepository;

        public UserTicketService(IMappingRepository<UserTicket, string, Guid> userTicketRepository, IRepository<Ticket, Guid> ticketRepository)
        {
            this.userTicketRepository = userTicketRepository;
            this.ticketRepository = ticketRepository;
        }

        public async Task BuyTicketAsync(string userId, Guid ticketId)
        {
            var ticket = await ticketRepository.GetAllAttached().Include(t => t.Concert).FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
            {
                throw new ArgumentException("Error 404");
            }

            var userTickets = await userTicketRepository
                .GetAllAttached()
                .Where(ut => ut.TicketId == ticketId)
                .Where(ut => ut.UserId == userId)
                .FirstOrDefaultAsync();

            if (userTickets == null)
            {
                if (ticket.Concert.OrganizerId == userId)
                {
                    throw new ArgumentException("Error 403");
                }
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
            if (userTicket == null)
            {
                throw new ArgumentException("Error 404");
            }
            await userTicketRepository.DeleteAsync(userTicket);
        }

        public async Task RemoveTicketAsync(string userId, Guid ticketId)
        {
            var userTicket = await userTicketRepository.GetAllAttached().Where(ut => ut.TicketId == ticketId && ut.UserId == userId).FirstOrDefaultAsync();
            if (userTicket == null)
            {
                throw new ArgumentException("Error 404");
            }
            await userTicketRepository.DeleteAsync(userTicket);
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
