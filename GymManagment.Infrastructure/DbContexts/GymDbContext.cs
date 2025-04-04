using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.DbContexts
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base (options)
        {
        }
        public DbSet <Client> Clients { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("clients","Gym");
            modelBuilder.Entity<Client>()
                .Property(c => c.Id)
                .HasColumnName("id")
                .HasColumnType("integer");
            modelBuilder.Entity<Client>()
                .Property(c => c.FirstName)
                .HasColumnName("first_name")
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Client>()
                .Property(c => c.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Client>()
                .Property (c => c.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Client>()
                .Property(c => c.PhoneNumber)
                .HasColumnName("phone_number")
                .IsRequired()
                .HasMaxLength(11);
            modelBuilder.Entity<Client>()
                .Property(c => c.Gender)
                .HasColumnName("gender")
                .HasMaxLength(11);
            modelBuilder.Entity<Client>()
                .Property(c => c.DateOfBirth)
                .HasColumnName("date_of_birth")
                .HasColumnType("date");
            modelBuilder.Entity<Client>()
                .Property(c => c.RegistrationDate)
                .HasColumnName("registration_date")
                .HasColumnType("date");
         
            modelBuilder.Entity<Membership>(entity =>
            {
                entity.ToTable("memberships", "Gym");
                entity.Property(m => m.Id)
                .HasColumnName("id")
                .HasColumnType("integer");
                entity.Property(m => m.MembershipType)
                .HasColumnName("membership_type")
                .IsRequired();
                entity.Property(m => m.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)");
            
            }); 
            base.OnModelCreating(modelBuilder);
        }
    }
}

