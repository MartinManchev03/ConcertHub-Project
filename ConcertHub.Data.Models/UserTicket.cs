using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class UserTicket
    {
        public Guid UserId { get; set; }
        public IdentityUser User { get; set; }

        public Guid TicketId { get; set; }

        public Ticket Ticket { get; set; }
    }
}
