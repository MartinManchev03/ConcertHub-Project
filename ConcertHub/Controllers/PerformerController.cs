using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
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
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
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
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await performerService.GetPerformerForEditAsync(id);
            return View(model);
        }

        [HttpPost]
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
            var model = await this.performerService.GetPerformerDetailsAsync(id);

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await this.performerService.GetPerformerForDeleteAsync(id);
            return View(model);
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
