using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class TicketType
    {
        public TicketType()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
