using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GymManagment.Infrastructure.Configurations
{
    public class PaymentClientMembershipConfiguration : IEntityTypeConfiguration<PaymentClientMembership>
    {
        public void Configure(EntityTypeBuilder<PaymentClientMembership> builder)
        {
            builder.ToTable("payment_client_membership", "Gym");
            builder.HasKey(pcm => new { pcm.PaymentId, pcm.ClientMembershipId });

            builder.Property(pcm => pcm.PaymentId)
                .HasColumnName("payment_id");

            builder.Property(pcm => pcm.ClientMembershipId)
                .HasColumnName("client_membership_id");

            builder.HasOne(pcm => pcm.Payment)
                .WithMany()
                .HasForeignKey(pcm => pcm.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pcm => pcm.ClientMembership)
                .WithMany()
                .HasForeignKey(pcm => pcm.ClientMembershipId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
