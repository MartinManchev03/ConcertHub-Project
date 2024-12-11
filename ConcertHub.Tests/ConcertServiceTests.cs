using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertHub.Data.Repository;
using ConcertHub.Data.Models;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.Services.Data;
using ConcertHub.ViewModels;
using PagedList;
using Microsoft.AspNetCore.Identity;
namespace ConcertHub.Tests
{
    public class ConcertServiceTests
    {
        private List<Concert> concerts = new List<Concert>
            {
            new Concert { Id = Guid.Parse("47fa55a9-a623-40e2-809b-bd1e5c125201"), ConcertName = "Concert1", Category = new Category { Name = "Rock" }, StartDate = DateTime.Now, EndDate = DateTime.Now, Organizer = new IdentityUser {Id = "user1", UserName = "Organizer1" } }
            };

        private readonly Mock<IRepository<Concert, Guid>> _mockConcertRepository;
        private readonly Mock<IMappingRepository<UserTicket, string, Guid>> _mockUserTicketRepository;
        private readonly Mock<IRepository<Category, Guid>> _mockCategoryRepository;
        private readonly Mock<IRepository<Ticket, Guid>> _mockTicketRepository;
        private readonly Mock<IMappingRepository<ConcertPerformer, string, Guid>> _mockConcertPerformerRepository;
        private readonly IConcertService _concertService;

        public ConcertServiceTests()
        {
            _mockConcertRepository = new Mock<IRepository<Concert, Guid>>();
            _mockUserTicketRepository = new Mock<IMappingRepository<UserTicket, string, Guid>>();
            _mockCategoryRepository = new Mock<IRepository<Category, Guid>>();
            _mockTicketRepository = new Mock<IRepository<Ticket, Guid>>();
            _mockConcertPerformerRepository = new Mock<IMappingRepository<ConcertPerformer, string, Guid>>();

            _concertService = new ConcertService(
                _mockConcertRepository.Object,
                _mockUserTicketRepository.Object,
                _mockCategoryRepository.Object,
                _mockTicketRepository.Object,
                _mockConcertPerformerRepository.Object
            );
        }
        [Test]
        public async Task GetAllConcerts_ShouldReturnPagedList()
        {
            var pagedConcerts = concerts.ToPagedList(1, 6);
           
            _mockConcertRepository.Setup(repo => repo.GetAllAttached()).Returns(pagedConcerts.AsQueryable());
            _mockUserTicketRepository.Setup(repo => repo.GetAllAttached()).Returns(new List<UserTicket>().AsQueryable());
            _mockCategoryRepository.Setup(repo => repo.GetAllAttached()).Returns(new List<Category>().AsQueryable());

            var result = _concertService.GetAllConcerts(1, "user1");

            Assert.NotNull(result);
            Assert.That(result.Count == 1);
            Assert.That(result[0].ConcertName == "Concert1");
            
        }

        [Test]
        public async Task AddConcertAsync()
        {
            ConcertViewModel model = new ConcertViewModel()
            {
                ConcertName = "Concert2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Location = "Stara Zagora",
                Description = ".......................................",
                CategoryId = Guid.Parse("47fa55a9-a623-40e2-809b-bd1e5c222222")
            };

            Guid expectedId = Guid.NewGuid();

            _mockConcertRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Concert>()))
                .Callback<Concert>(concert => concert.Id = expectedId);

            var concertId = await _concertService.AddConcertAsync(model, "user1");

            _mockConcertRepository.Verify(repo => repo.AddAsync(It.Is<Concert>(concert =>
                concert.ConcertName == model.ConcertName &&
                concert.StartDate == model.StartDate &&
                concert.EndDate == model.EndDate &&
                concert.Location == model.Location &&
                concert.Description == model.Description &&
                concert.CategoryId == model.CategoryId &&
                concert.OrganizerId == "user1"
            )), Times.Once);

            Assert.AreNotEqual(Guid.Empty, concertId);
            Assert.AreEqual(expectedId, concertId);

        }

        [Test]
        public async Task EditConcertAsync_ShouldUpdateConcert()
        {
            var originalConcert = new Concert
            {
                Id = Guid.Parse("47fa55a9-a623-40e2-809b-bd1e5c125201"),
                ConcertName = "Original Name",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Location = "Original Location",
                Description = "Original Description",
                CategoryId = Guid.Parse("47fa55a9-a623-40e2-809b-bd1e5c125202"),
                OrganizerId = "user1"
            };

            var updatedViewModel = new ConcertViewModel
            {
                Id = originalConcert.Id,
                ConcertName = "Updated Name",
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(2),
                Location = "Updated Location",
                Description = "Updated Description",
                CategoryId = Guid.Parse("47fa55a9-a623-40e2-809b-bd1e5c222222")
            };

            _mockConcertRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(originalConcert);

            _mockConcertRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Concert>()))
                .Returns(Task.CompletedTask);

            await _concertService.EditConcertAsync(updatedViewModel);

            _mockConcertRepository.Verify(repo => repo.UpdateAsync(It.Is<Concert>(concert =>
                concert.ConcertName == updatedViewModel.ConcertName &&
                concert.StartDate == updatedViewModel.StartDate &&
                concert.EndDate == updatedViewModel.EndDate &&
                concert.Location == updatedViewModel.Location &&
                concert.Description == updatedViewModel.Description &&
                concert.CategoryId == updatedViewModel.CategoryId &&
                concert.OrganizerId == "user1"
            )), Times.Once);
        }

        [Test]
        public void DummyTest()
        {
            
        }
    }
}
