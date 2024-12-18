﻿using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Security.Claims;

namespace ConcertHub.Controllers
{
    public class PerformerController : Controller
    {
        private readonly IPerformerService performerService;

        public PerformerController(IPerformerService performerService)
        {
            this.performerService = performerService;
        }
        public IActionResult All(int? page)
        {
            var pagedPerformers =  performerService.GetAllPerformers(page);
            return View(pagedPerformers);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PerformerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await performerService.AddPerformerAsync(viewModel, GetCurrentUserId());

            return RedirectToAction("All");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = await performerService.GetPerformerForEditAsync(id, GetCurrentUserId());
                return View(model);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PerformerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await performerService.EditPerformerAsync(viewModel);
            return RedirectToAction("All");



        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var model = await this.performerService.GetPerformerDetailsAsync(id);
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var model = await this.performerService.GetPerformerForDeleteAsync(id, GetCurrentUserId());
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await this.performerService.DeletePerformerAsync(id);
            return RedirectToAction("All");
        }

        [HttpGet]
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
