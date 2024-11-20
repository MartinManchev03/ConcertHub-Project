using ConcertHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertHub.Data.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);

            builder
              .HasOne(tt => tt.TicketType)
              .WithMany()
              .HasForeignKey(tt => tt.TicketTypeId);

            builder
                .HasOne(c => c.Concert)
                .WithMany()
                .HasForeignKey(c => c.ConcertId);

        }
    }
}
