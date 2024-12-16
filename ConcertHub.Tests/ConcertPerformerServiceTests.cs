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
            _mockUserManager = new Mock<UserManager<IdentityUser>>();

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
    }
}
