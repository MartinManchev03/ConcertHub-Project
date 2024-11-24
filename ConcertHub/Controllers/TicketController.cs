using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
			var concertId = TempData["ConcertId"];
			var tickets = JsonSerializer.Deserialize<List<TicketsCheckBoxViewModel>>((string)TempData["Tickets"]);
            var ticketTypes = await context.TicketTypes.ToListAsync();
            foreach(var t in tickets)
            {
				if (t.IsChecked)
				{
                    var ticketType = ticketTypes.FirstOrDefault(tt => tt.Name == t.Name);
					var ticket = new Ticket()
					{
                        TicketTypeId = ticketType.Id,
						ConcertId = (Guid)concertId,
						IsUsed = false
					};
					await context.Tickets.AddAsync(ticket);
				}
            }
			await context.SaveChangesAsync();

			return RedirectToAction("All", "Concert");
        }
    }
}
