using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.Services.Data;
using ConcertHub.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Constraints;
using Microsoft.EntityFrameworkCore;

namespace ConcertHub.Tests
{
    public class ConcertPerformerServiceTests
    {
        private Mock<IMappingRepository<ConcertPerformer, string, Guid>> _mockConcertPerformerRepository;
        private Mock<IRepository<Performer, Guid>> _mockPerformerRepository;
        private Mock<IRepository<Concert, Guid>> _mockConcertRepository;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private IConcertPerformerService _concertPerformerService;
        [SetUp]
        public void Setup()
        {
            _mockConcertPerformerRepository = new Mock<IMappingRepository<ConcertPerformer, string, Guid>>();
            _mockPerformerRepository = new Mock<IRepository<Performer, Guid>>();
            _mockConcertRepository = new Mock<IRepository<Concert, Guid>>();

            _mockPerformerRepository = new Mock<IRepository<Performer, Guid>>();

            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            );

            _concertPerformerService = new ConcertPerformerService(
                _mockConcertPerformerRepository.Object,
                _mockPerformerRepository.Object,
                _mockConcertRepository.Object,
                _mockUserManager.Object
            );
        }

        [Test]
        public async Task AddConcertPerformerAsync_ShouldAddConcertPerformers()
        {
            var concertId = Guid.NewGuid();
            var performerId = Guid.NewGuid();

            var viewModel = new AddConcertPerformersViewModel()
            {
                ConcertId = concertId,
                ConcertPerformers = new List<ConcertPerformersCheckboxViewModel>
                {
                    new ConcertPerformersCheckboxViewModel { PerformerId = performerId, IsChecked = true }
                }
            };

            _mockConcertPerformerRepository
                .Setup(repo => repo.AddAsync(It.IsAny<ConcertPerformer>()));

            await _concertPerformerService.AddConcertPerformerAsync(viewModel);

            _mockConcertPerformerRepository.Verify(repo => repo.AddAsync(It.Is<ConcertPerformer>(cp =>
                cp.ConcertId == concertId &&
                cp.PerformerId == performerId
            )), Times.Once);
        }

        [Test]
        public void GetAllConcertPerformersAsync_ShouldThrowNotFoundExceptionIfConcertDoesNotExist()
        {
            var concertId = Guid.NewGuid();
            var userId = "user1";

            _mockConcertRepository.Setup(repo => repo.GetByIdAsync(concertId))
                .ReturnsAsync((Concert)null);

            Assert.ThrowsAsync<ArgumentException>(() => _concertPerformerService.GetAllConcertPerformersAsync(concertId, userId), "Error 404");
        }
        [Test]
        public async Task GetAllConcertPerformersAsync_ShouldReturnList()
        {
            var concertId = Guid.NewGuid();
            var userId = "user1";
            var concert = new Concert
            {
                Id = concertId,
                OrganizerId = userId,
                CategoryId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ConcertName = "Concert 1",
                Description = ".............................",
                Location = "Stara Zagora..............",
                IsDeleted = false
  
            };
            var performers = new List<Performer>
            {
                new Performer { Id = Guid.NewGuid(), PerformerName = "Performer 1", Bio = "............................", StageName = "Best", CreatorId = userId,
                ConcertPerformers = new List<ConcertPerformer>()
                }
            };
            var concertPerformers = new List<ConcertPerformer>
            {
                new ConcertPerformer { ConcertId = concertId, PerformerId = performers[0].Id}
            };
            var userManager = new Mock<UserManager<IdentityUser>>();
            userManager.Setup(s => s.FindByIdAsync(userId))
           .ReturnsAsync(new IdentityUser { Id = userId, UserName = "TestUser1" });
            _mockConcertRepository.Setup(repo => repo.GetByIdAsync(concertId)).ReturnsAsync(concert);
            _mockPerformerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(performers);
            _mockConcertPerformerRepository.Setup(repo => repo.GetAllAttached())
                .Returns(concertPerformers.AsQueryable);

            var result = await _concertPerformerService.GetAllConcertPerformers(concertId, userId);
            Assert.IsNotNull(result);
            Assert.That(result.ConcertId == concertId);
 

        }

        [Test]
        public void GetAllConcertPerformersAsync_ShouldThrowExceptionIfUserIsNotOrganizer()
        {
            var concertId = Guid.NewGuid();
            var userId = "user1";
            var concert = new Concert
            {
                Id = concertId,
                OrganizerId = userId,
                CategoryId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ConcertName = "Concert 1",
                Description = ".............................",
                Location = "Stara Zagora..............",
                IsDeleted = false

            };
            var performers = new List<Performer>
            {
                new Performer { Id = Guid.NewGuid(), PerformerName = "Performer 1", Bio = "............................", StageName = "Best", CreatorId = userId,
                ConcertPerformers = new List<ConcertPerformer>()
                }
            };
            var concertPerformers = new List<ConcertPerformer>
            {
                new ConcertPerformer { ConcertId = concertId, PerformerId = performers[0].Id}
            };

            _mockConcertRepository.Setup(repo => repo.GetByIdAsync(concertId)).ReturnsAsync(concert);
            _mockPerformerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(performers);
            _mockConcertPerformerRepository.Setup(repo => repo.GetAllAttached())
                .Returns(concertPerformers.AsQueryable);

            Assert.ThrowsAsync<ArgumentException>(() => _concertPerformerService.GetAllConcertPerformersAsync(concertId, "invalidUser"), "Error 403");
        }
        [Test]
        public void DeleteConcertPerformerAsync_ShouldThrowErrorForInvalidConcertPerformer()
        {
            var performerId = Guid.NewGuid();
            var concertId = Guid.NewGuid();
            _mockConcertPerformerRepository.Setup(repo => repo.DeleteAsync(It.IsAny<string>(), It.IsAny<Guid>()));


            Assert.ThrowsAsync<ArgumentException>(() =>_concertPerformerService.RemoveConcertPerformer(performerId, concertId, "userId"), "Error 404");
        }
        [Test]
        public void DeleteConcertPerformerAsync_ShouldThrowErrorForOrganizerDifferentFromCurrentUser()
        {
            var performerId = Guid.NewGuid();
            var concertId = Guid.NewGuid();
            var userId = "user1";

            var concert = new Concert()
            {
                Id = concertId,
                ConcertName = "ConcertName",
                Location = "stara zagora somwewhere",
                Description = "......................",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                CategoryId = Guid.NewGuid(),
                IsDeleted = false,
                OrganizerId = userId
            };

            var performer = new Performer()
            {
                Id = performerId,
                Bio = "....................",
                CreatorId = userId,
                PerformerName = "Performer1",
                StageName = "StageName1"
            };

            var list = new List<ConcertPerformer>()
            {
                new ConcertPerformer {Concert = concert, Performer = performer}
            };
            _mockConcertPerformerRepository.Setup(repo => repo.GetAllAttached()).Returns(list.AsQueryable);

            _mockUserManager.Setup(s => s.FindByIdAsync(userId))
           .ReturnsAsync(new IdentityUser { Id = userId, UserName = "TestUser1" });

            Assert.ThrowsAsync<ArgumentException>(() => _concertPerformerService.RemoveConcertPerformer(performerId, concertId, "invalidUser"), "Error 404");
        }
    }
}
