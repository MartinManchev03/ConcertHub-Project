using ConcertHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class MyTicketsViewModel
    {
        public Guid Id { get; set; }

        public TicketType TicketType { get; set; }

        public string ConcertName { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}
