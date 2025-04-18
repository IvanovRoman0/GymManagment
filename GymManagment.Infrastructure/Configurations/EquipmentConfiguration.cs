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
    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.ToTable("equipment", "Gym");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.EquipmentName)
                .HasColumnName("equipment_name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.GymId)
                .HasColumnName("gym_id")
                .IsRequired();

            builder.Property(e => e.DatePurchase)
                .HasColumnName("date_purchase")
                .HasColumnType("date");

        }
    }
}
