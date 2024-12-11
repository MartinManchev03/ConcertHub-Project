using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.Services.Data;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConcertHub.Controllers
{
    [Authorize]
    public class UserTicketController : Controller
    {
        private readonly IUserTicketService userTicketService;

        public UserTicketController(IUserTicketService userTicketService)
        {
            this.userTicketService = userTicketService;
        }
        public async Task<IActionResult> All()
        {
            var usersTickets = await userTicketService.GetMyTicketsAsync(GetCurrentUserId());
            return View(usersTickets);
        }

        public async Task<IActionResult> Buy(Guid id)
        {
            try
            {
                await userTicketService.BuyTicketAsync(GetCurrentUserId(), id);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message = ex.Message });
            }

        }
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await userTicketService.RemoveTicketAsync(GetCurrentUserId(), id);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message = ex.Message });
            }
        }
        public async Task<IActionResult> Leave(Guid id)
        {
            try
            {
                await userTicketService.LeaveConcertAsync(GetCurrentUserId(), id);
                return RedirectToAction("All", "Concert");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message =  ex.Message });
            }
		}

        public async Task<IActionResult> Join(Guid id)
        {
            var isJoin = await userTicketService.UpdateUserTicket(GetCurrentUserId(), id);
            if (!isJoin)
            {
                return RedirectToAction("All", "Ticket");
            }
            return RedirectToAction("All", "Concert");
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
