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
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
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
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity != null)
            {
                _context.Classes.Remove(classEntity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Classes
                .AnyAsync(c => c.Id == id, cancellationToken);
        }
    }
}
