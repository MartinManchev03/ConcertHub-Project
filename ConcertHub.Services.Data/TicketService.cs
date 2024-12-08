using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace ConcertHub.Services.Data
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket, Guid> ticketRepository;
        private readonly IMappingRepository<UserTicket,string, Guid> userTicketRepository;
        private readonly IRepository<TicketType, Guid> ticketTypeRepository;
        
        public TicketService(IRepository<Ticket, Guid> ticketRepository, IMappingRepository<UserTicket,string, Guid> userTicketRepository, IRepository<TicketType, Guid> ticketTypeRepository)
        {
            this.ticketRepository = ticketRepository;
            this.userTicketRepository = userTicketRepository;
            this.ticketTypeRepository = ticketTypeRepository;
        }
        public IPagedList<TicketsViewModel> GetAllTickets(int? page, string currentUserId)
        {
            var tickets = this.ticketRepository
                .GetAllAttached()
                 .Select(t => new TicketsViewModel()
                 {
                     Id = t.Id,
                     TicketType = t.TicketType,
                     ConcertName = t.Concert.ConcertName,
                     Organizer = t.Concert.Organizer.UserName,
                     HasTicket = userTicketRepository.GetAllAttached()
                                        .Any(ut => ut.Ticket.ConcertId == t.ConcertId && ut.UserId == currentUserId)
                 })
                 .OrderBy(t => t.ConcertName)
                 .ThenByDescending(t => t.TicketType.Price);

            int pageSize = 10;
            int pageNumber = page ?? 1;
            var pagedTickets = tickets.ToPagedList(pageNumber, pageSize);

            return pagedTickets;
        }

        public async Task AddTicketsAsync(string concertEntry, Guid concertId, List<TicketsCheckBoxViewModel> tickets)
        {

            var ticketTypes = await ticketTypeRepository.GetAllAsync();
            if (concertEntry == "Paid")
            {
                foreach (var t in tickets)
                {
                    if (t.IsChecked)
                    {
                        var ticketType = ticketTypes.FirstOrDefault(tt => tt.Name == t.Name);
                        var ticket = CreateTicket(concertId, ticketType.Id);
                        await ticketRepository.AddAsync(ticket);
                    }
                }
            }
            else
            {
                var ticketType = ticketTypes.FirstOrDefault(tt => tt.Name == "Free");
                var ticket = CreateTicket(concertId, ticketType.Id);
                await ticketRepository.AddAsync(ticket);
            }
        }

        public async Task EditTicketsAsync(string concertEntry, Guid concertId, List<TicketsCheckBoxViewModel> tickets)
        {
            var ticketTypes = await ticketTypeRepository.GetAllAsync();
            var concertTickets = await ticketRepository.GetAllAttached().Where(t => t.ConcertId == concertId).ToListAsync();
            bool isAlreadyAdded = false;
            for (int i = 0; i < tickets.Count; i++)
            {
                if (!concertTickets.Any(ct => ct.TicketType.Name == tickets[i].Name) && tickets[i].IsChecked == true && concertEntry == "Paid")
                {
                    var ticketType = ticketTypes.FirstOrDefault(tt => tt.Name == tickets[i].Name);
                    var ticket = CreateTicket(concertId, ticketType.Id);
                    await ticketRepository.AddAsync(ticket);
                }
                if (!concertTickets.Any(ct => ct.TicketType.Name == "Free") && concertEntry == "Free" && isAlreadyAdded == false)
                {
                    var ticketType = ticketTypes.FirstOrDefault(tt => tt.Name == "Free");
                    var ticket = CreateTicket(concertId, ticketType.Id);
                    await ticketRepository.AddAsync(ticket);
                    isAlreadyAdded = true;
                }
                if ((concertTickets.Any(ct => ct.TicketType.Name == tickets[i].Name) && tickets[i].IsChecked == false) || concertEntry == "Free" && concertTickets.Any(ct => ct.TicketType.Name == tickets[i].Name))
                {
                    var ticket = await ticketRepository.GetAllAttached().FirstOrDefaultAsync(t => t.TicketType.Name == tickets[i].Name && t.ConcertId == concertId);
                    await ticketRepository.DeleteAsync(ticket.Id);
                }
                else if (concertEntry == "Paid" && concertTickets.Any(ct => ct.TicketType.Name == "Free"))
                {
                    var ticket = await ticketRepository.GetAllAttached().FirstOrDefaultAsync(t => t.TicketType.Name == "Free" && t.ConcertId == concertId);
                    await ticketRepository.DeleteAsync(ticket.Id);
                }

            }
        }

        private Ticket CreateTicket(Guid concertId, Guid ticketTypeId)
        {
            var ticket = new Ticket()
            {
                TicketTypeId = ticketTypeId,
                ConcertId = concertId
            };
            return ticket;
        }
    }
}
