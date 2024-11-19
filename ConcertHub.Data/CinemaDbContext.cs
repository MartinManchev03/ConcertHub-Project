using ConcertHub.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConcertHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Concert> Concerts { get; set; }

        public virtual DbSet<Performer> Performers { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<FeedBack> FeedBacks { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<ConcertPerformer> ConcertsPerformers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
