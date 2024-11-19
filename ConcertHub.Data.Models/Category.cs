
namespace ConcertHub.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Name { get; set; }

        public ICollection<Concert> Concerts { get; set; } = new HashSet<Concert>();
    }
}
