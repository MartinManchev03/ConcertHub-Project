using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class DeleteConcertViewModel
    {
        public Guid Id { get; set; }

        public string ConcertName { get; set; }

        public string Organizer { get; set; }
    }
}
