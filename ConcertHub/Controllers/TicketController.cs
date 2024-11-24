using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net.Sockets;
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
        public async Task<IActionResult> All()
        {

            return View();
        }

        public async Task<IActionResult> Add()
        {
            var concertEntry = TempData["ConcertEntry"];
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




        private Ticket CreateTicket(Guid concertId, Guid ticketTypeId)
        {
			var ticket = new Ticket()
			{
				TicketTypeId = ticketTypeId,
				ConcertId = concertId,
				IsUsed = false
			};
            return ticket;
		}
    }
}
