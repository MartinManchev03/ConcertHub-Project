
namespace ConcertHub.Data.Models
{
    public class Category
    {
        public Category()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Concert> Concerts { get; set; } = new List<Concert>();
    }
}
