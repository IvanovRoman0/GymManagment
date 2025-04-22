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
    public class ReviewRepository : IReviewRepository
    {
        private readonly GymDbContext _context;

        public ReviewRepository(GymDbContext context) => _context = context;

        public async Task<Reviews> GetByIdAsync(int id, CancellationToken cancellationToken)
            => await _context.Reviews
                .Include(r => r.Client)
                .Include(r => r.Trainer)
                .Include(r => r.Gym)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        public async Task<IEnumerable<Reviews>> GetAllAsync(CancellationToken cancellationToken)
            => await _context.Reviews
                .Include(r => r.Client)
                .Include(r => r.Trainer)
                .Include(r => r.Gym)
                .ToListAsync(cancellationToken);

        public async Task AddAsync(Reviews review, CancellationToken cancellationToken)
        {
            await _context.Reviews.AddAsync(review, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Reviews review, CancellationToken cancellationToken)
        {
            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
