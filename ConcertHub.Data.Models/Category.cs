
namespace ConcertHub.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Concert> Concerts { get; set; } = new HashSet<Concert>();
    }
}
