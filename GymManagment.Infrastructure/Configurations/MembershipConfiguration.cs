using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagment.Infrastructure.Configurations
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("memberships", "Gym");
            builder.Property(m => m.Id)
                .HasColumnName("id")
                .HasColumnType("integer");
            builder.Property(m => m.MembershipType)
                .HasColumnName("membership_type")
                .IsRequired();
            builder.Property(m => m.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)");
        }
    }
}
