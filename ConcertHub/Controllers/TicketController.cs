using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PagedList;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text.Json;

namespace ConcertHub.Controllers
{
    public class TicketController : Controller
    {
        private ApplicationDbContext context;
        public TicketController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> All(int? page)
        {
			var tickets = await context.Tickets
				.Select(t => new TicketsViewModel()
				{
					Id = t.Id,
					TicketType = t.TicketType,
					ConcertName = t.Concert.ConcertName,
                    Organizer = t.Concert.Organizer.UserName,
                    HasTicket = context.UsersTickets
                                       .Any(ut => ut.Ticket.ConcertId == t.ConcertId && ut.UserId == GetCurrentUserId())
                })
                .OrderBy(t => t.ConcertName)
                .ThenByDescending(t => t.TicketType.Price)
				.ToListAsync();
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var pagedTickets = tickets.ToPagedList(pageNumber, pageSize);
            return View(pagedTickets);
        }

        public async Task<IActionResult> Add()
        {
            var concertEntry = TempData["ConcertEntry"].ToString();
			var concertId = TempData["ConcertId"];
			var tickets = JsonSerializer.Deserialize<List<TicketsCheckBoxViewModel>>((string)TempData["Tickets"]);
            var ticketTypes = await context.TicketTypes.ToListAsync();
            if(concertEntry == "Paid")
            {
				foreach (var t in tickets)
				{
					if (t.IsChecked)  
					{
						var ticketType = ticketTypes.FirstOrDefault(tt => tt.Name == t.Name);
						var ticket = CreateTicket((Guid)concertId, ticketType.Id);
						await context.Tickets.AddAsync(ticket);
					}
				}
			}
            else
            {
				var ticketType = ticketTypes.FirstOrDefault(tt => tt.Name == "Free");
				var ticket = CreateTicket((Guid)concertId, ticketType.Id);
				await context.Tickets.AddAsync(ticket);
			}
			await context.SaveChangesAsync();

			return RedirectToAction("All", "Concert");
        }

		public async Task<IActionResult> Edit()
		{
            var concertEntry = TempData["ConcertEntry"].ToString();
            var concertId = TempData["ConcertId"];
            var tickets = JsonSerializer.Deserialize<List<TicketsCheckBoxViewModel>>((string)TempData["Tickets"]);
			tickets[2].IsChecked = true;
            var ticketTypes = await context.TicketTypes.ToListAsync();
			var concertTickets = await context.Tickets.Where(t => t.ConcertId == (Guid)concertId).ToListAsync();
			bool isAlreadyAdded = false;
			for (int i = 0; i < tickets.Count; i++)
			{
                if (!concertTickets.Any(ct => ct.TicketType.Name == tickets[i].Name) && tickets[i].IsChecked == true && concertEntry == "Paid")
                {
                    var ticketType = ticketTypes.FirstOrDefault(tt => tt.Name == tickets[i].Name);
                    var ticket = CreateTicket((Guid)concertId, ticketType.Id);
                    await context.Tickets.AddAsync(ticket);
                }
                if (!concertTickets.Any(ct => ct.TicketType.Name == "Free") && concertEntry == "Free" && isAlreadyAdded == false)
                {
                    var ticketType = ticketTypes.FirstOrDefault(tt => tt.Name == "Free");
                    var ticket = CreateTicket((Guid)concertId, ticketType.Id);
                    await context.Tickets.AddAsync(ticket);
                    isAlreadyAdded = true;
                }
                if ((concertTickets.Any(ct => ct.TicketType.Name == tickets[i].Name) && tickets[i].IsChecked == false) || concertEntry == "Free" && concertTickets.Any(ct => ct.TicketType.Name == tickets[i].Name))
				{
					var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.TicketType.Name == tickets[i].Name && t.ConcertId == (Guid)concertId);
				    context.Tickets.Remove(ticket);
				}
				else if(concertEntry == "Paid" && concertTickets.Any(ct => ct.TicketType.Name == "Free"))
				{
                    var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.TicketType.Name == "Free" && t.ConcertId == (Guid)concertId);
                    context.Tickets.Remove(ticket);
                }

			}
            await context.SaveChangesAsync();

            return RedirectToAction("All", "Concert");
        }
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
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
