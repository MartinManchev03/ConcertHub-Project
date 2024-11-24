using ConcertHub.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.ViewModels
{
    public class EditConcertViewModel
    {

        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string ConcertName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Location { get; set; }
        [Required]
        [MaxLength(500)]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}
