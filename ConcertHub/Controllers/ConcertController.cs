﻿using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json;

namespace ConcertHub.Controllers
{
    public class ConcertController : Controller
    {
        private readonly ApplicationDbContext context;
        public ConcertController(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> All(int? page)
        {
            var concerts = await context.Concerts
                .Where(c => c.IsDeleted == false)
                .Select(c => new AllConcertsViewModel()
                {
                    Id = c.Id,
                    Category = c.Category.Name,
                    ConcertName = c.ConcertName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Location = c.Location,
                    Organizer = c.Organizer.UserName,
                    IsJoined = context.UsersTickets
                              .Any(ut => ut.UserId == GetCurrentUserId() && ut.Ticket.ConcertId == c.Id && ut.IsUsed == true)

                })
                .ToListAsync();
            int pageSize = 6;
            int pageNumber = page ?? 1;
            var pagedConcerts = concerts.ToPagedList(pageNumber, pageSize);
            return View(pagedConcerts);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await context.Categories.ToListAsync();
            var model = new ConcertViewModel();
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.Categories = categories;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ConcertViewModel model)
        {
			model.Categories = await context.Categories.ToListAsync();
            model.Tickets[2].IsChecked = true;
            if (!ModelState.IsValid)
            {
   
				return View(model);
			}
            var concert = new Concert()
            {
                ConcertName = model.ConcertName,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Location = model.Location,
                CategoryId = model.CategoryId,
                OrganizerId = GetCurrentUserId()
            };
            await context.Concerts.AddAsync(concert);
            await context.SaveChangesAsync();

            TempData["ConcertEntry"] = model.ConcertEntry;
			TempData["ConcertId"] = concert.Id;
			TempData["Tickets"] = JsonSerializer.Serialize(model.Tickets);
			return RedirectToAction("Add", "Ticket");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await context.Concerts
                .Select(c => new ConcertViewModel()
                {
                    Id = c.Id,
                    ConcertName = c.ConcertName,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Location = c.Location,
                    CategoryId = c.CategoryId,
                    Categories = context.Categories.ToList()
                })
                .FirstOrDefaultAsync(c => c.Id == id);
            var tickets = await context.Tickets.Where(t => t.ConcertId == id).Select(t => t.TicketType).ToListAsync();
            model.ConcertEntry = "Free";
            for (int i = 0; i < model.Tickets.Count; i++)
            {
                if (tickets.Any(t => t.Name == model.Tickets[i].Name))
                {
                    model.ConcertEntry = "Paid";
                    model.Tickets[i].IsChecked = true;
                }
            }
            TempData["ConcertEntry"] = model.ConcertEntry;
            TempData["ConcertId"] = model.Id;
            TempData["Tickets"] = JsonSerializer.Serialize(model.Tickets);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ConcertViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var model = await context.Concerts.FindAsync(viewModel.Id);

            model.ConcertName = viewModel.ConcertName;
            model.StartDate = viewModel.StartDate;
            model.EndDate = viewModel.EndDate;
            model.Description = viewModel.Description;
            model.Location = viewModel.Location;
            model.CategoryId = viewModel.CategoryId;

            await context.SaveChangesAsync();

            TempData["ConcertEntry"] = viewModel.ConcertEntry;
            TempData["ConcertId"] = viewModel.Id;
            TempData["Tickets"] = JsonSerializer.Serialize(viewModel.Tickets);
            return RedirectToAction("Edit", "Ticket");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await context.Concerts
                .Where(p => p.Id == id)
                .Select(p => new DeleteViewModel()
                {
                    Id = p.Id,
                    Name = p.ConcertName,
                    Creator = p.Organizer.UserName,
                    ControllerName = "Concert"
                })
                .FirstOrDefaultAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var model = await context.Concerts.FindAsync(id);
            var concertPerfomers = await context.ConcertsPerformers.Where(cp => cp.ConcertId == id).ToListAsync();
            for (int i = 0; i < concertPerfomers.Count; i++)
            {
                context.ConcertsPerformers.Remove(concertPerfomers[i]);
            }
            context.Concerts.Remove(model);
            await context.SaveChangesAsync();
            return RedirectToAction("All");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var concert = await context.Concerts
                .Include(c => c.Organizer)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            var viewModel = new ConcertDetailsViewModel
            {
                Id = concert.Id,
                ConcertName = concert.ConcertName,
                Description = concert.Description,
                StartDate = concert.StartDate,
                EndDate = concert.EndDate,
                Location = concert.Location,
                OrganizerName = concert.Organizer.UserName,
                CategoryName = concert.Category.Name,
				ConcertPerformers = new ConcertPerformersViewModel()
				{
					ConcertId = id,
                    Organizer = concert.Organizer.UserName,
					PerformersNames = context.ConcertsPerformers.Where(cp => cp.ConcertId == id)
                    .Select(cp => new PerformerConcertViewModel()
                    {
                        PerformerId = cp.PerformerId,
                        PerformerName = cp.Performer.PerformerName
                    })
                    .ToList()
				},
			};

            return View(viewModel);
        }
        public IActionResult Back()
        {
            return RedirectToAction("All");
        }
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
