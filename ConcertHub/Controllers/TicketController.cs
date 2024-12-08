using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PagedList;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text.Json;

namespace ConcertHub.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }
        public IActionResult All(int? page)
        {
            var pagedTickets = this.ticketService.GetAllTickets(page, GetCurrentUserId());
            return View(pagedTickets);
        }

        public async Task<IActionResult> Add()
        {
            var concertEntry = TempData["ConcertEntry"].ToString();
            var concertId = TempData["ConcertId"];
            var tickets = JsonSerializer.Deserialize<List<TicketsCheckBoxViewModel>>((string)TempData["Tickets"]);

            await ticketService.AddTicketsAsync(concertEntry, (Guid)concertId, tickets);

            return RedirectToAction("All", "Concert");
        }

		public async Task<IActionResult> Edit()
		{
            var concertEntry = TempData["ConcertEntry"].ToString();
            var concertId = TempData["ConcertId"];
            var tickets = JsonSerializer.Deserialize<List<TicketsCheckBoxViewModel>>((string)TempData["Tickets"]);
			tickets[2].IsChecked = true;

            await ticketService.EditTicketsAsync(concertEntry, (Guid)concertId, tickets);

            return RedirectToAction("All", "Concert");
        }
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
