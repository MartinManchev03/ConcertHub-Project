using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace ConcertHub.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ManagerController : Controller
    {
        private readonly IManagerService managerService;
        public ManagerController(IManagerService managerService)
        {
            this.managerService = managerService;
        }
        public async Task<IActionResult> Index(int? page)
        {
           var pagedUsers = await this.managerService.GetAllUsers(page);
            return View(pagedUsers);
        }
        
        public async Task<IActionResult> Add(string userId)
        {
            try
            {
                await managerService.AddManagerByIdAndAsync(userId);
            }
            catch (Exception message)
            {
                return RedirectToAction("Error", "Error", new { area = "", message = message.Message });
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(string userId)
        {
            try
            {
                await managerService.RemoveManagerByIdAsync(userId);
            }
            catch(Exception message)
            {
                return RedirectToAction("Error", "Error", new { area = "", message = message.Message });
            }
            return RedirectToAction("Index");
        }
    }
}
