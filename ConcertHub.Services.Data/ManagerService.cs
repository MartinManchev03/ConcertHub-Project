using ConcertHub.Data.Models;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data
{
    public class ManagerService : IManagerService
    {
        private readonly UserManager<IdentityUser> userManager;

        public ManagerService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }


        public async Task<IPagedList<ManagerUsersViewModel>> GetAllUsers(int? page)
        {
            var userViewModels = new List<ManagerUsersViewModel>();
            foreach (var user in userManager.Users.ToList())
            {
                var isManager = await userManager.IsInRoleAsync(user, "Manager");
                userViewModels.Add(new ManagerUsersViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = isManager
                });
            }
            int pageSize = 5;
            int pageNumber = page ?? 1;
            var pagedUsers = userViewModels.ToPagedList(pageNumber, pageSize);
            return pagedUsers;
        }

        public async Task AddManagerByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                throw new ArgumentException("Error 404");
            }
            await userManager.AddToRoleAsync(user, "Manager");
        }

        public async Task RemoveManagerByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("Error 404");
            }
            await userManager.RemoveFromRoleAsync(user, "Manager");
        }
    }
}
