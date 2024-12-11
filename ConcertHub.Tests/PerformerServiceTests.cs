using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Tests
{
    public class PerformerServiceTests
    {
        private Mock<IRepository<Performer, Guid>> _mockPerformerRepository;
        private PerformerService _performerService;

        [SetUp]
        public void Setup()
        {
            _mockPerformerRepository = new Mock<IRepository<Performer, Guid>>();
            _performerService = new PerformerService(_mockPerformerRepository.Object);
        }

        [Test]
        public async Task AddPerformerAsync_ShouldAddPerformer()
        {
            var viewModel = new PerformerViewModel
            {
                PerformerName = "Test Performer",
                StageName = "Test Stage",
                Bio = "Test Bio"
            };

            var expectedId = Guid.NewGuid();

            _mockPerformerRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Performer>()))
                .Callback<Performer>(p => p.Id = expectedId);

            await _performerService.AddPerformerAsync(viewModel, "user1");

            _mockPerformerRepository.Verify(repo => repo.AddAsync(It.Is<Performer>(p =>
                p.PerformerName == viewModel.PerformerName &&
                p.StageName == viewModel.StageName &&
                p.Bio == viewModel.Bio &&
                p.CreatorId == "user1"
            )), Times.Once);

            Assert.AreNotEqual(Guid.Empty, expectedId);
        }

        [Test]
        public async Task GetPerformerForEditAsync_ShouldReturnPerformerForEdit()
        {
            var performerId = Guid.NewGuid();
            var userId = "user1";

            var performer = new Performer
            {
                Id = performerId,
                PerformerName = "Test Performer",
                StageName = "Test Stage",
                Bio = "Test Bio",
                CreatorId = userId
            };

            _mockPerformerRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(performer);

            var result = await _performerService.GetPerformerForEditAsync(performerId, userId);

            Assert.NotNull(result);
            Assert.AreEqual("Test Performer", result.PerformerName);
        }

        [Test]
        public async Task GetPerformerForEditAsync_ShouldThrowExceptionIfUnauthorized()
        {
            var performerId = Guid.NewGuid();
            var unauthorizedUserId = "user2";

            var performer = new Performer
            {
                Id = performerId,
                PerformerName = "Test Performer",
                StageName = "Test Stage",
                Bio = "Test Bio",
                CreatorId = "user1"
            };

            _mockPerformerRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(performer);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => _performerService.GetPerformerForEditAsync(performerId, unauthorizedUserId));
            Assert.AreEqual("Error 403", ex.Message);
        }

        [Test]
        public async Task EditPerformerAsync_ShouldUpdatePerformer()
        {
            var performerId = Guid.NewGuid();

            var performer = new Performer
            {
                Id = performerId,
                PerformerName = "Original Name",
                StageName = "Original Stage",
                Bio = "Original Bio"
            };

            var viewModel = new PerformerViewModel
            {
                Id = performerId,
                PerformerName = "Updated Name",
                StageName = "Updated Stage",
                Bio = "Updated Bio"
            };

            _mockPerformerRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(performer);

            _mockPerformerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Performer>()))
                .Returns(Task.CompletedTask);

            await _performerService.EditPerformerAsync(viewModel);

            _mockPerformerRepository.Verify(repo => repo.UpdateAsync(It.Is<Performer>(p =>
                p.PerformerName == viewModel.PerformerName &&
                p.StageName == viewModel.StageName &&
                p.Bio == viewModel.Bio
            )), Times.Once);
        }

        [Test]
        public async Task DeletePerformerAsync_ShouldDeletePerformer()
        {
            var performerId = Guid.NewGuid();

            _mockPerformerRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()));

            await _performerService.DeletePerformerAsync(performerId);

            _mockPerformerRepository.Verify(repo => repo.DeleteAsync(performerId), Times.Once);
        }

    }
}
