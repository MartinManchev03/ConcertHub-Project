using ConcertHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class TicketsViewModel
    {
        public Guid Id { get; set; }

        public TicketType TicketType { get; set; }

        public string ConcertName { get; set; }

        public string Organizer { get; set; }

        public bool HasTicket { get; set; } = false;

    }
}
