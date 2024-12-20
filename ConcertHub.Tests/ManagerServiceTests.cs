using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Tests
{
    public class ManagerServiceTests
    {
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private ManagerService _managerService;

        [SetUp]
        public void Setup()
        {
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
            _managerService = new ManagerService(_mockUserManager.Object);
        }
        [Test]
        public async Task GetAllUsers_ShouldReturnPagedListOfUsers()
        {
            var users = new List<IdentityUser>
            {
                new IdentityUser { Id = "user1", Email = "test1@example.com" },
                new IdentityUser { Id = "user2", Email = "test2@example.com" }
            };

            _mockUserManager.Setup(um => um.Users).Returns(users.AsQueryable());
            _mockUserManager.Setup(um => um.IsInRoleAsync(It.IsAny<IdentityUser>(), "Manager")).ReturnsAsync(false);

            int pageSize = 5;
            int pageNumber = 1;

            var result = await _managerService.GetAllUsers(pageNumber);

            Assert.That(result.PageSize, Is.EqualTo(pageSize));
            Assert.That(result.TotalItemCount, Is.EqualTo(users.Count));
            Assert.That(result[0].Id, Is.EqualTo(users[0].Id));
            Assert.That(result[0].Email, Is.EqualTo(users[0].Email));
            Assert.That(result[0].Role, Is.EqualTo(false));
        }

        [Test]
        public async Task AddManagerByIdAsync_ShouldAddManagerRole()
        {
            var userId = "user1";
            var user = new IdentityUser { Id = userId, UserName = "TestUser1" };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.AddToRoleAsync(user, "Manager")).ReturnsAsync(IdentityResult.Success);

            await _managerService.AddManagerByIdAsync(userId);

            _mockUserManager.Verify(um => um.AddToRoleAsync(user, "Manager"), Times.Once);
        }

        [Test]
        public async Task RemoveManagerByIdAsync_ShouldRemoveManagerRole()
        {
            var userId = "user1";
            var user = new IdentityUser { Id = userId, UserName = "TestUser1" };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.RemoveFromRoleAsync(user, "Manager")).ReturnsAsync(IdentityResult.Success);

            await _managerService.RemoveManagerByIdAsync(userId);

            _mockUserManager.Verify(um => um.RemoveFromRoleAsync(user, "Manager"), Times.Once);
        }
    }
}
