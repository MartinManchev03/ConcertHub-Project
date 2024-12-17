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
    public class UserTicketServiceTests
    {
        private Mock<IMappingRepository<UserTicket, string, Guid>> _mockUserTicketRepository;
        private Mock<IRepository<Ticket, Guid>> _mockTicketRepository;
        private UserTicketService _userTicketService;

        [SetUp]
        public void Setup()
        {
            _mockUserTicketRepository = new Mock<IMappingRepository<UserTicket, string, Guid>>();
            _mockTicketRepository = new Mock<IRepository<Ticket, Guid>>();
            _userTicketService = new UserTicketService(_mockUserTicketRepository.Object,_mockTicketRepository.Object);
        }

        [Test]
        public void GetMyTickets_ShouldReturnList()
        {
            var userId = "user1";
            var concert = new Concert
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
            var tickets = new List<UserTicket>
            {
                new UserTicket
                {
                    UserId = userId,
                    Ticket = new Ticket
                    {
                        Id = Guid.NewGuid(),
                        Concert = concert,
                        TicketType = new TicketType(){ Id = Guid.NewGuid(), Name = "General", Price = 30},
                    },
                    IsUsed = false
                }
            };

            _mockUserTicketRepository.Setup(repo => repo.GetAllAttached()).Returns(tickets.AsQueryable());


            var result = _userTicketService.GetMyTickets(userId);
            var r = result.ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(r[0].Id, Is.EqualTo(tickets[0].Ticket.Id));
            Assert.That(r[0].ConcertName, Is.EqualTo(tickets[0].Ticket.Concert.ConcertName));
            Assert.That(r[0].TicketType, Is.EqualTo(tickets[0].Ticket.TicketType));
            Assert.That(r[0].IsUsed, Is.EqualTo(tickets[0].IsUsed));
        }
    }
}
