

using Microsoft.AspNetCore.Identity;

namespace ConcertHub.Data.Models
{
    public class Concert
    {
        public Concert()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public string ConcertName { get; set; }

        public string Description { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public string OrganizerId { get; set; }

        public IdentityUser Organizer { get; set; }

        public Guid CategoryId { get; set; }
        public  Category Category { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<ConcertPerformer> ConcertPerformers { get; set; } = new List<ConcertPerformer>();
       
        public ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
