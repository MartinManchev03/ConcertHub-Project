using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
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
    public class PerformerService : IPerformerService
    {
        private readonly IRepository<Performer, Guid> repository;
        private readonly UserManager<IdentityUser> userManager;
        public PerformerService(IRepository<Performer, Guid> repository, UserManager<IdentityUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }


        public IPagedList<AllPerformersViewModel> GetAllPerformers(int? page)
        {
            var performers = this.repository
            .GetAllAttached()
            .Select(p => new AllPerformersViewModel()
            {
                PerformerName = p.PerformerName,
                StageName = p.StageName,
                Id = p.Id,
                Creator = p.Creator.UserName
            });

            int pageSize = 5;
            int pageNumber = page ?? 1;
            var pagedPerformers = performers.ToPagedList(pageNumber, pageSize);

            return pagedPerformers;
        }

        public async Task AddPerformerAsync(PerformerViewModel viewModel, string creatorId)
        {
            var model = new Performer()
            {
                PerformerName = viewModel.PerformerName,
                StageName = viewModel.StageName,
                Bio = viewModel.Bio,
                CreatorId = creatorId
            };
            await this.repository.AddAsync(model);
        }

        public async Task<PerformerViewModel> GetPerformerForEditAsync(Guid id, string userId)
        {
            var model = await this.repository.GetByIdAsync(id);

            var user = await userManager.FindByIdAsync(userId);

            if(model == null)
            {
                throw new ArgumentException("Error 404");
            }
            if(model.CreatorId != userId && !await userManager.IsInRoleAsync(user, "Manager"))
            {
                throw new ArgumentException("Error 403");
            }

            PerformerViewModel viewModel = new PerformerViewModel()
            {
                Id = model.Id,
                PerformerName = model.PerformerName,
                Bio = model.Bio,
                StageName = model.StageName
            };

            return viewModel;
        }

        public async Task EditPerformerAsync(PerformerViewModel viewModel)
        {
            var model = await this.repository.GetByIdAsync(viewModel.Id);
            model.PerformerName = viewModel.PerformerName;
            model.StageName = viewModel.StageName;
            model.Bio = viewModel.Bio;
            await this.repository.UpdateAsync(model);
        }


        public async Task<DetailsPerformerViewModel> GetPerformerDetailsAsync(Guid id)
        {
            var model = await this.repository
                .GetAllAttached()
                .Include(c => c.Creator)
                .Select(p => new DetailsPerformerViewModel()
                {
                    Id = p.Id,
                    PerformerName = p.PerformerName,
                    Bio = p.Bio,
                    StageName = p.StageName,
                    Creator = p.Creator.UserName
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if(model == null)
            {
                throw new ArgumentException("Error 404");
            }
            

            return model;
        }

        public async Task DeletePerformerAsync(Guid id)
        {
            await this.repository.DeleteAsync(id);
        }

        public async Task<DeleteViewModel> GetPerformerForDeleteAsync(Guid id, string userId)
        {
            var model = await this.repository
                .GetAllAttached()
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(p => p.Id == id);

            var user = await userManager.FindByIdAsync(userId);

            if (model == null)
            {
                throw new ArgumentException("Error 404");
            }
            if (model.CreatorId != userId && !await userManager.IsInRoleAsync(user, "Manager"))
            {
                throw new ArgumentException("Error 403");
            }

            var viewModel = new DeleteViewModel()
            {
                Id = model.Id,
                Name = model.PerformerName,
                Creator = model.Creator.UserName,
                ControllerName = "Performer"
            };

            return viewModel;
        }

    }
}
