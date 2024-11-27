using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConcertHub.Controllers
{
    public class UserTicketController : Controller
    {
        private ApplicationDbContext context;

        public UserTicketController(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public async Task<IActionResult> All()
        {
            var usersTickets = await context.UsersTickets
                                     .Where(ut => ut.UserId == GetCurrentUserId())
                                     .Select(ut => new MyTicketsViewModel()
                                     {
                                         Id = ut.Ticket.Id,
                                         ConcertName = ut.Ticket.Concert.ConcertName,
                                         TicketType = ut.Ticket.TicketType,
                                         IsUsed = ut.IsUsed
                                     })
                                     .ToListAsync();
            return View(usersTickets);
        }

        public async Task<IActionResult> Buy(Guid id)
        {
            var userId = GetCurrentUserId();
            var userTickets = await context.UsersTickets.Where(ut => ut.TicketId == id).Where(ut => ut.UserId == userId).FirstOrDefaultAsync();
            if(userTickets == null)
            {
                var userTicket = new UserTicket()
                {
                    UserId = userId,
                    TicketId = id
                };
                await context.AddAsync(userTicket);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("All");
        }
        public async Task<IActionResult> Remove(Guid id)
        {
            var userId = GetCurrentUserId();
            var userTicket = await context.UsersTickets.Where(ut => ut.TicketId == id).Where(ut => ut.UserId == userId).FirstOrDefaultAsync();
            if (userTicket != null)
            {
                context.Remove(userTicket);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("All");
        }

        public async Task<IActionResult> Join(Guid id)
        {
            var userTicket = await context.UsersTickets
            .Where(ut => ut.Ticket.ConcertId == id && ut.UserId == GetCurrentUserId())
            .FirstOrDefaultAsync();

            if(userTicket == null)
            {
                return RedirectToAction("All", "Ticket");
            }
            userTicket.IsUsed = true;
            await context.SaveChangesAsync();
            return RedirectToAction("All", "Concert");
        }

        public async Task<IActionResult> Leave(Guid id)
        {
			var userTicket = await context.UsersTickets
			.Where(ut => ut.Ticket.ConcertId == id && ut.UserId == GetCurrentUserId())
			.FirstOrDefaultAsync();
            
            context.UsersTickets.Remove(userTicket);
            await context.SaveChangesAsync();
			return RedirectToAction("All", "Concert");
		}
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
