using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.Services.Data.Interfaces;
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
        private readonly IConcertService concertService;

        public ConcertController(IConcertService concertService)
        {
            this.concertService = concertService;
        }

        public IActionResult All(int? page)
        {
            var pagedConcerts = concertService.GetAllConcerts(page, GetCurrentUserId());
            return View(pagedConcerts);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await concertService.GetConcertForAddAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ConcertViewModel model)
        {
            if (!ModelState.IsValid)
            {
				return View(model);
			}
            Guid concertId = await concertService.AddConcertAsync(model, GetCurrentUserId());

            TempData["ConcertEntry"] = model.ConcertEntry;
			TempData["ConcertId"] = concertId;
			TempData["Tickets"] = JsonSerializer.Serialize(model.Tickets);
			return RedirectToAction("Add", "Ticket");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await concertService.GetConcertForEditAsync(id);
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

            await concertService.EditConcertAsync(viewModel);

            TempData["ConcertEntry"] = viewModel.ConcertEntry;
            TempData["ConcertId"] = viewModel.Id;
            TempData["Tickets"] = JsonSerializer.Serialize(viewModel.Tickets);
            return RedirectToAction("Edit", "Ticket");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await concertService.GetConcertForDeleteAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await concertService.DeleteConcertAsync(id);
            return RedirectToAction("All");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var viewModel = await concertService.GetConcertDetailsAsync(id);
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
