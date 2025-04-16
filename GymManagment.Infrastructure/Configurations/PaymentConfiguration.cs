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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments", "Gym");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.ClientId)
                .HasColumnName("client_id")
                .IsRequired();

            builder.Property(p => p.PaymentDate)
                .HasColumnName("payment_date")
                .HasColumnType("date");

            builder.Property(p => p.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.PaymentType)
                .HasColumnName("payment_type")
                .HasMaxLength(20)
                .HasConversion<string>();

            builder.HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
