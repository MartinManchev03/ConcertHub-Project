using ConcertHub.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static ConcertHub.Common.Constraints.TicketTypeConstraints;

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

        public virtual DbSet<TicketType> TicketTypes { get; set; }

        public virtual DbSet<FeedBack> FeedBacks { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<ConcertPerformer> ConcertsPerformers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<Category>()
                .HasData(
                    new Category { Id = Guid.NewGuid(), Name = "Rock",},
                    new Category { Id = Guid.NewGuid(), Name = "Pop", },
                    new Category { Id = Guid.NewGuid(), Name = "Classical", },
                    new Category { Id = Guid.NewGuid(), Name = "Jazz", },
                    new Category { Id = Guid.NewGuid(), Name = "Hip-Hop", },
                    new Category { Id = Guid.NewGuid(), Name = "Country", },
                    new Category { Id = Guid.NewGuid(), Name = "Latin", },
                    new Category { Id = Guid.NewGuid(), Name = "Folk", }
                );


            builder.Entity<TicketType>()
                .HasData(
                    new TicketType { Id = Guid.NewGuid(), Name = "Free", Price = FreeTicketPrice },
                    new TicketType { Id = Guid.NewGuid(), Name = "General", Price = GeneralTicketPrice },
                    new TicketType { Id = Guid.NewGuid(), Name = "Regural", Price = RegularTicketPrice },
                    new TicketType { Id = Guid.NewGuid(), Name = "VIP", Price = VIPTicketPrice });
        }
    }
}
