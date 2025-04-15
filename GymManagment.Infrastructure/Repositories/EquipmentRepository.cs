using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GymManagment.Infrastructure.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly GymDbContext _context;

        public EquipmentRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<Equipment> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Equipment
                .Include(e => e.Gym)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Equipment>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Equipment
                .Include(e => e.Gym)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Equipment>> GetByGymIdAsync(int gymId, CancellationToken cancellationToken)
        {
            return await _context.Equipment
                .Include(e => e.Gym)
                .Where(e => e.GymId == gymId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Equipment equipment, CancellationToken cancellationToken)
        {
            await _context.Equipment.AddAsync(equipment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Equipment equipment, CancellationToken cancellationToken)
        {
            _context.Entry(equipment).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment != null)
            {
                _context.Equipment.Remove(equipment);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Equipment
                .AnyAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<bool> NameExistsInGymAsync(string name, int gymId, CancellationToken cancellationToken)
        {
            return await _context.Equipment
                .AnyAsync(e => e.EquipmentName == name && e.GymId == gymId, cancellationToken);
        }
    }
}
