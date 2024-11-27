using ConcertHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertHub.Data.Configuration
{
    public class UserTicketConfiguration : IEntityTypeConfiguration<UserTicket>
    {
        public void Configure(EntityTypeBuilder<UserTicket> builder)
        {
            builder.HasKey(ut => new {ut.UserId, ut.TicketId});

            builder
                .HasOne(ut => ut.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(ut => ut.Ticket)
                .WithMany(u => u.UserTickets)
                .HasForeignKey(ut => ut.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
