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
    public class ClientMembershipConfiguration : IEntityTypeConfiguration<ClientMembership>
    {
        public void Configure(EntityTypeBuilder<ClientMembership> builder)
        {
            builder.ToTable("client_membership", "Gym");
            builder.HasKey(cm => cm.id);

            builder.Property(cm => cm.id)
                .HasColumnName("id")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(cm => cm.ClientId)
                .HasColumnName("client_id")
                .IsRequired();

            builder.Property(cm => cm.MembershipId)
                .HasColumnName("membership_id")
                .IsRequired();

            builder.Property(cm => cm.DateStart)
                .HasColumnName("date_start")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(cm => cm.DateEnd)
                .HasColumnName("date_end")
                .HasColumnType("date")
                .IsRequired();

            builder.HasOne(cm => cm.Client)
                .WithMany()
                .HasForeignKey(cm => cm.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cm => cm.Membership)
                .WithMany()
                .HasForeignKey(cm => cm.MembershipId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
