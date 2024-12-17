using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data
{
    public class ConcertPerformerService : IConcertPerformerService
    {
        private readonly IMappingRepository<ConcertPerformer, string, Guid> concertPerformerRepository;
        private readonly IRepository<Performer, Guid> performerRepository;
        private readonly IRepository<Concert, Guid> concertRepository;
        private readonly UserManager<IdentityUser> userManager;

        public ConcertPerformerService(IMappingRepository<ConcertPerformer, string, Guid> concertPerformerRepository, IRepository<Performer, Guid> performerRepository, IRepository<Concert, Guid> concertRepository, UserManager<IdentityUser> userManager)
        {
            this.concertPerformerRepository = concertPerformerRepository;
            this.performerRepository = performerRepository;
            this.concertRepository = concertRepository;
            this.userManager = userManager;
        }

        public async Task AddConcertPerformerAsync(AddConcertPerformersViewModel viewModel)
        {
            foreach (var p in viewModel.ConcertPerformers)
            {
                if (p.IsChecked)
                {
                    var cp = new ConcertPerformer()
                    {
                        PerformerId = p.PerformerId,
                        ConcertId = viewModel.ConcertId
                    };
                    await concertPerformerRepository.AddAsync(cp);
                }
            }
        }

        public async Task<AddConcertPerformersViewModel> GetAllConcertPerformersAsync(Guid concertId, string userId)
        {
            var concert = await concertRepository.GetByIdAsync(concertId);
            if (concert == null)
            {
                throw new ArgumentException("Error 404");
            }

            if (!await IsAuthorizedToPerformAction(concert.OrganizerId, userId))
            {
                throw new ArgumentException("Error 403");
            }
            var concertPerformers = new AddConcertPerformersViewModel()
            {
                ConcertId = concertId
            };

            var alreadyAssociatedPerformerIds = await concertPerformerRepository
                .GetAllAttached()
                .Where(cp => cp.ConcertId == concertId)
                .Select(cp => cp.PerformerId)
                .ToListAsync();

            foreach (var p in await performerRepository.GetAllAsync())
            {
                if (!alreadyAssociatedPerformerIds.Contains(p.Id))
                {
                    concertPerformers.ConcertPerformers.Add(new ConcertPerformersCheckboxViewModel()
                    {
                        PerformerId = p.Id,
                        PerformerName = p.PerformerName
                    });
                }
            }

            return concertPerformers;
        }
        public async Task<AddConcertPerformersViewModel> GetAllConcertPerformers(Guid concertId, string userId)
        {
            var concert = await concertRepository.GetByIdAsync(concertId);
            if (concert == null)
            {
                throw new ArgumentException("Error 404");
            }

            if (!await IsAuthorizedToPerformAction(concert.OrganizerId, userId))
            {
                throw new ArgumentException("Error 403");
            }
            var concertPerformers = new AddConcertPerformersViewModel()
            {
                ConcertId = concertId
            };

            var alreadyAssociatedPerformerIds = concertPerformerRepository
                .GetAllAttached()
                .Where(cp => cp.ConcertId == concertId)
                .Select(cp => cp.PerformerId)
                .ToList();

            foreach (var p in await performerRepository.GetAllAsync())
            {
                if (!alreadyAssociatedPerformerIds.Contains(p.Id))
                {
                    concertPerformers.ConcertPerformers.Add(new ConcertPerformersCheckboxViewModel()
                    {
                        PerformerId = p.Id,
                        PerformerName = p.PerformerName
                    });
                }
            }

            return concertPerformers;
        }


        public async Task<ConcertPerformersViewModel> RemoveConcertPerformerAsync(Guid performerId, Guid concertId, string userId)
        {
            var concertPerformer = await concertPerformerRepository.
                GetAllAttached()
                .Where(cp => cp.ConcertId == concertId && cp.PerformerId == performerId)
                .Include(c => c.Concert)
                .Include(c => c.Concert.Organizer)
                .FirstOrDefaultAsync();

            if(concertPerformer == null)
            {
                throw new ArgumentException("Error 404");
            }
            if (!await IsAuthorizedToPerformAction(concertPerformer.Concert.OrganizerId, userId))
            {
                throw new ArgumentException("Error 403");
            }
            await concertPerformerRepository.DeleteAsync(concertPerformer);

            var concertPerformers = new ConcertPerformersViewModel()
            {
                ConcertId = concertId,
                Organizer = concertPerformer.Concert.Organizer.UserName,
                PerformersNames = await concertPerformerRepository.GetAllAttached().Where(cp => cp.ConcertId == concertId)
                    .Select(cp => new PerformerConcertViewModel()
                    {
                        PerformerId = cp.PerformerId,
                        PerformerName = cp.Performer.PerformerName
                    })
                    .ToListAsync()
            };
            return concertPerformers;   

        }
        public async Task<ConcertPerformersViewModel> RemoveConcertPerformer(Guid performerId, Guid concertId, string userId)
        {
            var concertPerformer = concertPerformerRepository.
                GetAllAttached()
                .Where(cp => cp.ConcertId == concertId && cp.PerformerId == performerId)
                .Include(c => c.Concert)
                .Include(c => c.Concert.Organizer)
                .FirstOrDefault();

            if (concertPerformer == null)
            {
                throw new ArgumentException("Error 404");
            }
            if (!await IsAuthorizedToPerformAction(concertPerformer.Concert.OrganizerId, userId))
            {
                throw new ArgumentException("Error 403");
            }
            await concertPerformerRepository.DeleteAsync(concertPerformer);

            var concertPerformers = new ConcertPerformersViewModel()
            {
                ConcertId = concertId,
                Organizer = concertPerformer.Concert.Organizer.UserName,
                PerformersNames = await concertPerformerRepository.GetAllAttached().Where(cp => cp.ConcertId == concertId)
                    .Select(cp => new PerformerConcertViewModel()
                    {
                        PerformerId = cp.PerformerId,
                        PerformerName = cp.Performer.PerformerName
                    })
                    .ToListAsync()
            };
            return concertPerformers;

        }

        private async Task<bool> IsAuthorizedToPerformAction(string organizerId, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (organizerId != userId && !await userManager.IsInRoleAsync(user, "Manager") && !await userManager.IsInRoleAsync(user, "Admin"))
            {
                return false;
            }
            return true;
        }
    }
}
