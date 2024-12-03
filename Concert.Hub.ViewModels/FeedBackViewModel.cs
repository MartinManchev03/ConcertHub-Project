using ConcertHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class FeedBackViewModel
    {
        public Guid ConcertId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
