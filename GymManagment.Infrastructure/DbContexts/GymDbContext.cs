﻿using GymManagement.Core.Entities;
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
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<ClientMembership> ClientMemberships { get; set; }
        public DbSet<RegistrationClass> RegistrationClasses { get; set; }
        public DbSet<PaymentClientMembership> PaymentClientMemberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
        }
    }
}

