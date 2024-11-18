using ConcertHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConcertHub.Common.EntityValidationConstraints.TicketValidation;
namespace ConcertHub.Data.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TicketType)
                .IsRequired()
                .HasMaxLength(TicketTypeMaxLength);

            builder.HasOne(c => c.Concert)
                .WithMany()
                .HasForeignKey(c => c.ConcertId);

            builder.HasOne(c => c.Buyer)
                .WithMany()
                .HasForeignKey(c => c.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
