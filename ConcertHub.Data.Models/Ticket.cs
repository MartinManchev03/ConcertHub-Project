using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class Ticket
    {
        public Ticket()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public Guid TicketTypeId { get; set; }

        public TicketType TicketType { get; set; }

        public Guid ConcertId { get; set; }

        public Concert Concert { get; set; }

        public bool IsUsed { get; set; } = false;

        public ICollection<UserTicket> UserTickets { get; set; } = new List<UserTicket>();
    }
}
