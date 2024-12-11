using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace ConcertHub.Controllers
{
    public class ConcertPerformerController : Controller
    {
        private readonly IConcertPerformerService concertPerformerService;

        public ConcertPerformerController(IConcertPerformerService concertPerformerService)
        {
            this.concertPerformerService = concertPerformerService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add(Guid concertId)
        {
            try
            {
                var concertPerformers = await concertPerformerService.GetAllConcertPerformersAsync(concertId, GetCurrentUserId());
                return View(concertPerformers);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddConcertPerformersViewModel viewModel)
        {
            await concertPerformerService.AddConcertPerformerAsync(viewModel);
            return RedirectToAction("Details", "Concert", new {id = viewModel.ConcertId});
        }

        [Authorize]
        public async Task<IActionResult> Remove(Guid performerId, Guid concertId)
        {
            try
            {
                var concertPerformers = await concertPerformerService.RemoveConcertPerformerAsync(performerId, concertId, GetCurrentUserId());
                return PartialView("_AllConcertPerformers", concertPerformers);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Error", new { message = ex.Message });
            }
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
