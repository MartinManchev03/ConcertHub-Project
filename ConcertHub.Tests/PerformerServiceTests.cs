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
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private PerformerService _performerService;

        [SetUp]
        public void Setup()
        {
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

            _mockPerformerRepository = new Mock<IRepository<Performer, Guid>>();
            _performerService = new PerformerService(_mockPerformerRepository.Object, _mockUserManager.Object);
        }

        [Test]
        public void GetAllPerformers_ShouldReturnPagedListOfPerformers()
        {

            var performers = new List<Performer>
            {
                 new Performer { Id = Guid.NewGuid(), PerformerName = "Performer1", StageName = "Stage1", Creator = new IdentityUser { UserName = "Creator1" } },
                 new Performer { Id = Guid.NewGuid(), PerformerName = "Performer2", StageName = "Stage2", Creator = new IdentityUser { UserName = "Creator2" } }
             };

            _mockPerformerRepository.Setup(repo => repo.GetAllAttached()).Returns(performers.AsQueryable());

            int pageSize = 5;
            int pageNumber = 1;

            var result = _performerService.GetAllPerformers(pageNumber);

            Assert.That(result.PageSize, Is.EqualTo(pageSize));
            Assert.That(result.TotalItemCount, Is.EqualTo(performers.Count));
            Assert.That(result[0].PerformerName, Is.EqualTo(performers[0].PerformerName));
            Assert.That(result[0].StageName, Is.EqualTo(performers[0].StageName));
            Assert.That(result[0].Creator, Is.EqualTo(performers[0].Creator.UserName));
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
            var userId = "user1";
            var performerId = Guid.NewGuid();
            var performer = new Performer
            {
                Id = performerId,
                PerformerName = "Test Performer",
                StageName = "Test Stage",
                Bio = "Test Bio....................",
                CreatorId = "user1"
                
            };
            _mockUserManager.Setup(s => s.FindByIdAsync(userId))
                .ReturnsAsync(new IdentityUser { Id = userId, UserName = "TestUser1" });

            _mockPerformerRepository.Setup(repo => repo.GetByIdAsync(performerId))
                                    .ReturnsAsync(performer);

            var result = await _performerService.GetPerformerForEditAsync(performerId, userId);

            Assert.That(result.Id, Is.EqualTo(performer.Id));
            Assert.That(result.PerformerName, Is.EqualTo(performer.PerformerName));
            Assert.That(result.StageName, Is.EqualTo(performer.StageName));
            Assert.That(result.Bio, Is.EqualTo(performer.Bio));
        }

        [Test]
        public async Task GetPerformerForEditAsync_ShouldThrowExceptionIfUnauthorized()
        {
            var performerId = Guid.NewGuid();
            var unauthorizedUserId = "user2";
            var userId = "user1";
            var performer = new Performer
            {
                Id = performerId,
                PerformerName = "Test Performer",
                StageName = "Test Stage",
                Bio = "Test Bio",
                CreatorId = "user1"
            };

            _mockUserManager.Setup(s => s.FindByIdAsync(userId))
           .ReturnsAsync(new IdentityUser { Id = userId, UserName = "TestUser1" });
            _mockPerformerRepository.Setup(repo => repo.GetByIdAsync(performerId))
                .ReturnsAsync(performer);
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _performerService.GetPerformerForEditAsync(performerId, unauthorizedUserId));
            Assert.AreEqual("Error 403", ex.Message);
        }
        [Test]
        public async Task GetPerformerForEditAsync_ShouldThrowExceptionIfPerformerIdIsInvalid()
        {
            var performerId = Guid.NewGuid();
            var userId = "user1";
            var performer = new Performer
            {
                Id = performerId,
                PerformerName = "Test Performer",
                StageName = "Test Stage",
                Bio = "Test Bio",
                CreatorId = "user1"
            };

            _mockUserManager.Setup(s => s.FindByIdAsync(userId))
           .ReturnsAsync(new IdentityUser { Id = userId, UserName = "TestUser1" });
            _mockPerformerRepository.Setup(repo => repo.GetByIdAsync(performerId))
                .ReturnsAsync(performer);
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _performerService.GetPerformerForEditAsync(Guid.NewGuid(), userId));
            Assert.AreEqual("Error 404", ex.Message);
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

        [Test]
        public async Task GetPerformerForDeleteAsync_ShouldThrowExceptionIfPerformerIsNull()
        {
            var performerId = Guid.NewGuid();
            var performer = new Performer()
            {
                Id = performerId,
                Bio = "........................",
                CreatorId = "user1",
                PerformerName = "Name1",
                StageName = "StageName1"
            };
            _mockPerformerRepository.Setup(p => p.AddAsync(performer));

            var ex = Assert.ThrowsAsync<ArgumentException>(() => _performerService.GetPerformerForDelete(Guid.NewGuid(), "user1"));
            Assert.AreEqual("Error 404", ex.Message);
        }
        [Test]
        public async Task GetPerformerForDeleteAsync_ShouldThrowExceptionIfUserIsNotOrganizer()
        {
            var userId = "invalidUser";
            var performerId = Guid.NewGuid();
            var list = new List<Performer>()
            {
                new Performer()
                {
                    Id = performerId,
                    Bio = "........................",
                    CreatorId = "user1",
                    PerformerName = "Name1",
                    StageName = "StageName1"
                }
            };

            _mockUserManager.Setup(s => s.FindByIdAsync(userId))
           .ReturnsAsync(new IdentityUser { Id = userId, UserName = "TestUser1" });

            _mockPerformerRepository.Setup(p => p.GetAllAttached()).Returns(list.AsQueryable);

            var ex = Assert.ThrowsAsync<ArgumentException>(() => _performerService.GetPerformerForDelete(performerId, userId));
            Assert.AreEqual("Error 403", ex.Message);
        }


    }
}
