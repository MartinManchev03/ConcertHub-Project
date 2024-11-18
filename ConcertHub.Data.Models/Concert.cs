using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class Concert
    {
        public int Id { get; set; }

        public string ConcertName { get; set; }

        public string Description { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public string OrganizerId { get; set; }

        public IdentityUser Organizer { get; set; }

        public int CategoryId { get; set; }
        public  Category Category { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<ConcertPerformer> ConcertPerformers { get; set; } = new HashSet<ConcertPerformer>();
       
        public ICollection<FeedBack> FeedBacks { get; set; } = new HashSet<FeedBack>();

        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
