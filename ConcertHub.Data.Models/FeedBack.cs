using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Models
{
    public class FeedBack
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }

        public string PostedById { get; set; }

        public IdentityUser PostedBy { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

    }
}
