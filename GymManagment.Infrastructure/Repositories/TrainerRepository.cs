using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using GymManagement.Infrastructure.Repositories;

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
            return await _context.Trainer.Include(testc => t.Specialization).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<Trainer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Trainer.Include(testc => t.Specialization).AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task AddAsync(Trainer trainer, CancellationToken cancellationToken)
        {
            await _context.Trainer.AddAsync(trainer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(Trainer trainer, CancellationToken cancellationToken)
        {
            _context.Entry(trainer).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var trainer = await _context.Trainer.FindAsync(id);
            if (trainer != null)
            {
                _context.Trainer.Remove(trainer);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Trainer.AnyAsync(x => x.Email == email, cancellationToken);
        }
    }
}
