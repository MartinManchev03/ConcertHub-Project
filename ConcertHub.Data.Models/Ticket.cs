using Microsoft.AspNet.Identity.EntityFramework;
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
        public string TicketType { get; set; }
        public Guid ConcertId { get; set; }
        public Concert Concert { get; set; }
        public string BuyerId { get; set; }

        public IdentityUser Buyer { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}
