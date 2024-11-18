using ConcertHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConcertHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Concert> Concerts { get; set; }

        public DbSet<Performer> Performers { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<FeedBack> FeedBacks { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ConcertPerformer> ConcertsPerformers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin>(entity =>
            {
                entity.HasKey(login => new { login.LoginProvider, login.ProviderKey });
            });

            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole>(entity =>
            {
                entity.HasKey(role => new { role.UserId, role.RoleId });
            });


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
