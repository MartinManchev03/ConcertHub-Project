using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Tests
{
    public class FeedbackServiceTests
    {
        private Mock<IRepository<Concert, Guid>> _mockConcertRepository;
        private Mock<IRepository<FeedBack, Guid>> _mockFeedbackRepository;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private FeedbackService _feedbackService;

        [SetUp]
        public void Setup()
        {
            _mockConcertRepository = new Mock<IRepository<Concert, Guid>>();
            _mockFeedbackRepository = new Mock<IRepository<FeedBack, Guid>>();
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
            _feedbackService = new FeedbackService(
                _mockFeedbackRepository.Object,
                _mockConcertRepository.Object,
                _mockUserManager.Object);
        }

        [Test]
        public async Task GetAllFeedBackAsync_ShouldThrowErrorIfInvalidConcertId()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _feedbackService.GetAllFeedbacksAsync(Guid.NewGuid()));
            Assert.AreEqual("Error 404", ex.Message);
        }
        [Test]
        public async Task AddFeedbackAsync_ShouldAddFeedbackAndReturnUpdatedFeedbackList()
        {
            var userId = "user1";
            var concertId = Guid.NewGuid();
            var model = new FeedBackViewModel
            {
                Comment = "Great concert!",
                Rating = 5,
                ConcertId = concertId
            };

            var concert = new Concert
            {
                Id = concertId,
                ConcertName = "Concert1",
                Organizer = new IdentityUser { Id = "user1", UserName = "Organizer1" }
            };

            var feedback = new FeedBack
            {
                Id = Guid.NewGuid(),
                Comment = model.Comment,
                Rating = model.Rating,
                ConcertId = model.ConcertId,
                PostedById = userId,
                PostedBy = new IdentityUser { Id = userId, UserName = "TestUser1" }
            };

            _mockConcertRepository.Setup(repo => repo.GetByIdAsync(concertId)).ReturnsAsync(concert);
            _mockFeedbackRepository.Setup(repo => repo.AddAsync(It.IsAny<FeedBack>())).ReturnsAsync(true);
            _mockFeedbackRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<FeedBack> { feedback }.AsQueryable());

            var result = await _feedbackService.AddFeedback(model, userId);
            var r = result.ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(r[0].Comment, Is.EqualTo(model.Comment));
            Assert.That(r[0].Rating, Is.EqualTo(model.Rating));
            Assert.That(r[0].PostedBy, Is.EqualTo("TestUser1"));
            Assert.That(r[0].ConcertOrganizer, Is.EqualTo(concert.Organizer.UserName));
        }
        [Test]
        public async Task GetAllFeedBackAsync_ShouldReturnListOfFeedbacks()
        {
            var userId = "user1";
            var user = new IdentityUser { Id = userId, UserName = "TestUser1" };

            var concert = new Concert()
            {
                Id = Guid.NewGuid(),
                ConcertName = "Concert1",
                Category = new Category { Name = "Rock" },
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Organizer = new IdentityUser { Id = "user1", UserName = "Organizer1" },
                Location = "...............",
                CategoryId = Guid.NewGuid(),
                Description = "................",
                IsDeleted = false,
            };

            _mockConcertRepository.Setup(repo => repo.GetByIdAsync(concert.Id)).ReturnsAsync(concert);

            var feedBacks = new List<FeedBack>()
            {
                new FeedBack()
                {
                    Id = Guid.NewGuid(),
                    ConcertId = concert.Id,
                    PostedBy = user,
                    Rating = 5,
                    Comment = "Comment"
                    
                }
            };
            _mockFeedbackRepository.Setup(repo => repo.GetAllAttached()).Returns(feedBacks.AsQueryable());

            var result = await _feedbackService.GetAllFeedbacks(concert.Id);
            var r = result.ToList();

            Assert.That(r.Count, Is.EqualTo(1));
            Assert.That(r[0].Id, Is.EqualTo(feedBacks[0].Id));
            Assert.That(r[0].Comment, Is.EqualTo(feedBacks[0].Comment));
            Assert.That(r[0].PostedBy, Is.EqualTo(feedBacks[0].PostedBy.UserName));
            Assert.That(r[0].Rating, Is.EqualTo(feedBacks[0].Rating));

        }



    }
}
