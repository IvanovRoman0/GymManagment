using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GymManagment.Infrastructure.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext _context;
        public TrainerRepository(GymDbContext context)
        {
            _context = context;
        }
        public async Task<Trainer> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Trainers.Include(t => t.Specialization).AsNoTracking().FirstOrDefaultAsync(c => c.id == id, cancellationToken);
        }
        public async Task<IEnumerable<Trainer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Trainers.Include(t => t.Specialization).AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task AddAsync(Trainer trainer, CancellationToken cancellationToken)
        {
            await _context.Trainers.AddAsync(trainer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(Trainer trainer, CancellationToken cancellationToken)
        {
            _context.Entry(trainer).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlRawAsync(
               "DELETE FROM \"Gym\".\"trainers\" WHERE \"id\" = {0}",
               id);
        }
        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Trainers.AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Trainers
                .AsNoTracking()
                .AnyAsync(t => t.id == id, cancellationToken);
        }
    }
}
