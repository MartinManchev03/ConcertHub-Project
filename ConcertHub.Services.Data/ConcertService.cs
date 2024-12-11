using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Services.Data
{
    public class ConcertService : IConcertService
    {
        private readonly IRepository<Concert, Guid> concertRepository;
        private readonly IMappingRepository<UserTicket, string, Guid> userTicketRepository;
        private readonly IMappingRepository<ConcertPerformer, string, Guid> concertPerformerRepository;
        private readonly IRepository<Category, Guid> categoryRepository;
        private readonly IRepository<Ticket, Guid> ticketRepository;

        public ConcertService(IRepository<Concert, Guid> concertRepository, IMappingRepository<UserTicket, string, Guid> userTicketRepository, IRepository<Category, Guid> categoryRepository, IRepository<Ticket, Guid> ticketRepository, IMappingRepository<ConcertPerformer, string, Guid> concertPerformerRepository)
        {
            this.concertRepository = concertRepository;
            this.userTicketRepository = userTicketRepository;
            this.categoryRepository = categoryRepository;
            this.ticketRepository = ticketRepository;
            this.concertPerformerRepository = concertPerformerRepository;
        }

        public IPagedList<AllConcertsViewModel> GetAllConcerts(int? page, string userId)
        {
            var concerts = concertRepository
                .GetAllAttached()
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
                    IsJoined = userTicketRepository.GetAllAttached()
                              .Any(ut => ut.UserId == userId && ut.Ticket.ConcertId == c.Id && ut.IsUsed == true)

                })
                .ToList();
            int pageSize = 6;
            int pageNumber = page ?? 1;
            var pagedConcerts = concerts.ToPagedList(pageNumber, pageSize);
            return pagedConcerts;
        }
        public async Task<ConcertViewModel> GetConcertForAddAsync()
        {
            var categories = await this.categoryRepository.GetAllAsync();
            var model = new ConcertViewModel();
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model.Categories = categories.ToList();

            return model;
        }

        public async Task<Guid> AddConcertAsync(ConcertViewModel model, string creatorId)
        {
            var concert = new Concert()
            {
                ConcertName = model.ConcertName,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Location = model.Location,
                CategoryId = model.CategoryId,
                OrganizerId = creatorId
            };
            await concertRepository.AddAsync(concert);
            return concert.Id;
        }

        public async Task<ConcertViewModel> GetConcertForEditAsync(Guid concertId, string userId)
        {
            var concert = await concertRepository
                .GetAllAttached()
                .Include(c => c.Organizer)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == concertId);

            if (concert == null)
            {
                throw new ArgumentException("Error 404");
            }
            if (userId != concert.OrganizerId)
            {
                throw new ArgumentException("Error 403");
            }

            var model = new ConcertViewModel()
            {
                Id = concert.Id,
                ConcertName = concert.ConcertName,
                Description = concert.Description,
                StartDate = concert.StartDate,
                EndDate = concert.EndDate,
                Location = concert.Location,
                CategoryId = concert.CategoryId,
                Categories = this.categoryRepository.GetAllAttached().ToList()
            };

            var tickets = await ticketRepository.GetAllAttached().Where(t => t.ConcertId == concertId).Select(t => t.TicketType).ToListAsync();
            model.ConcertEntry = "Free";
            for (int i = 0; i < model.Tickets.Count; i++)
            {
                if (tickets.Any(t => t.Name == model.Tickets[i].Name))
                {
                    model.ConcertEntry = "Paid";
                    model.Tickets[i].IsChecked = true;
                }
            }
            return model;
        }

        public async Task EditConcertAsync(ConcertViewModel viewModel)
        {
            var model = await concertRepository.GetByIdAsync(viewModel.Id);

            model.ConcertName = viewModel.ConcertName;
            model.StartDate = viewModel.StartDate;
            model.EndDate = viewModel.EndDate;
            model.Description = viewModel.Description;
            model.Location = viewModel.Location;
            model.CategoryId = viewModel.CategoryId;

            await concertRepository.UpdateAsync(model);
        }

        public async Task<DeleteViewModel> GetConcertForDeleteAsync(Guid concertId, string userId)
        {
            var concert = await concertRepository
                .GetAllAttached()
                .Include(c => c.Organizer)
                .FirstOrDefaultAsync(c => c.Id == concertId);

            if (concert == null)
            {
                throw new ArgumentException("Error 404");
            }
            if (userId != concert.OrganizerId)
            {
                throw new ArgumentException("Error 403");
            }

            var model = new DeleteViewModel()
            {
                Id = concert.Id,
                Name = concert.ConcertName,
                Creator = concert.Organizer.UserName,
                ControllerName = "Concert"
            };

            return model;
        }
        public async Task DeleteConcertAsync(Guid concertId)
        {
            var model = await concertRepository.GetByIdAsync(concertId);
            var concertPerfomers = await concertPerformerRepository.GetAllAttached().Where(cp => cp.ConcertId == concertId).ToListAsync();
            for (int i = 0; i < concertPerfomers.Count; i++)
            {
                await concertPerformerRepository.DeleteAsync(concertPerfomers[i]);
            }
            await concertRepository.DeleteAsync(concertId);
        }

        public async Task<ConcertDetailsViewModel> GetConcertDetailsAsync(Guid concertId)
        {
            var concert = await concertRepository
                .GetAllAttached()
                .Include(c => c.Organizer)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == concertId);

            if (concert == null)
            {
                throw new ArgumentException("Error 404");
            }

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
					ConcertId = concertId,
                    Organizer = concert.Organizer.UserName,
					PerformersNames = concertPerformerRepository.GetAllAttached().Where(cp => cp.ConcertId == concertId)
                    .Select(cp => new PerformerConcertViewModel()
                    {
                        PerformerId = cp.PerformerId,
                        PerformerName = cp.Performer.PerformerName
                    })
                    .ToList()
				},
			};
            return viewModel;
        }
    }

}
