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
    public class GymConfiguration : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.ToTable("gyms", "Gym");
            builder.Property(g => g.id)
                .ValueGeneratedOnAdd();

            builder.Property(g => g.GymName)
                .HasColumnName("gym_name")
                .HasMaxLength(100);

            builder.Property(g => g.Location)
                .HasColumnName("location")
                .HasMaxLength(100);
        }
    }
}
