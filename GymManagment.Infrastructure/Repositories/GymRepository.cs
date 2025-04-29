using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GymManagment.Infrastructure.Repositories
{
    public class GymRepository : IGymRepository
    {
        private readonly GymDbContext _context;

        public GymRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<Gym> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Gyms
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.id == id, cancellationToken);
        }

        public async Task<IEnumerable<Gym>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Gyms
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Gym gym, CancellationToken cancellationToken)
        {
            await _context.Gyms.AddAsync(gym, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Gym gym, CancellationToken cancellationToken)
        {
            _context.Entry(gym).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM \"Gym\".\"gyms\" WHERE \"id\" = {0}",
                id);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Gyms
                .AnyAsync(g => g.id == id, cancellationToken);
        }

        public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.Gyms
                .AnyAsync(g => g.GymName == name, cancellationToken);
        }
    }
}
