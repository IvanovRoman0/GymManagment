﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GymManagment.Infrastructure.Repositories
{
    public class RegistrationClassRepository : IRegistrationClassRepository
    {
        private readonly GymDbContext _context;

        public RegistrationClassRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<RegistrationClass> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.RegistrationClasses
                .Include(rc => rc.Client)
                .Include(rc => rc.Class)
                .AsNoTracking()
                .FirstOrDefaultAsync(rc => rc.id == id, cancellationToken);
        }

        public async Task<IEnumerable<RegistrationClass>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.RegistrationClasses
                .Include(rc => rc.Client)
                .Include(rc => rc.Class)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(RegistrationClass registrationClass, CancellationToken cancellationToken)
        {
            await _context.RegistrationClasses.AddAsync(registrationClass, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(RegistrationClass registrationClass, CancellationToken cancellationToken)
        {
            _context.Entry(registrationClass).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlRawAsync(
               "DELETE FROM \"Gym\".\"registrations_classes\" WHERE \"id\" = {0}",
               id);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.RegistrationClasses
                .AnyAsync(rc => rc.id == id, cancellationToken);
        }

        public async Task<bool> ExistsRegistrationAsync(int clientId, int classId, CancellationToken cancellationToken)
        {
            return await _context.RegistrationClasses
                .AnyAsync(rc => rc.ClientId == clientId && rc.ClassId == classId, cancellationToken);
        }
    }
}
