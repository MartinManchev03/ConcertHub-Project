using ConcertHub.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data.Interfaces
{
    public interface IManagerService
    {
        Task<IPagedList<ManagerUsersViewModel>> GetAllUsers(int? page);
        Task AddManagerByIdAsync(string userId);

        Task RemoveManagerByIdAsync(string userId);
    }
}
