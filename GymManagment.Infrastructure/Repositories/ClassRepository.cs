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
    public class ClassRepository : IClassRepository
    {
        private readonly GymDbContext _context;

        public ClassRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<Class> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Classes
                .Include(c => c.Trainer)
                .Include(c => c.Gym)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.id == id, cancellationToken);
        }

        public async Task<IEnumerable<Class>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Classes
                .Include(c => c.Trainer)
                .Include(c => c.Gym)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Class classEntity, CancellationToken cancellationToken)
        {
            await _context.Classes.AddAsync(classEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Class classEntity, CancellationToken cancellationToken)
        {
            _context.Entry(classEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlRawAsync(
               "DELETE FROM \"Gym\".\"class\" WHERE \"id\" = {0}",
               id);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Classes
                .AnyAsync(c => c.id == id, cancellationToken);
        }
    }
}
