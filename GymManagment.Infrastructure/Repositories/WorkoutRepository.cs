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
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly GymDbContext _context;

        public WorkoutRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<Workout> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Workouts
                .Include(w => w.Client)
                .Include(w => w.Gym)
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.id == id, cancellationToken);
        }

        public async Task<IEnumerable<Workout>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Workouts
                .Include(w => w.Client)
                .Include(w => w.Gym)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Workout workout, CancellationToken cancellationToken)
        {
            await _context.Workouts.AddAsync(workout, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Workout workout, CancellationToken cancellationToken)
        {
            _context.Entry(workout).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Workouts
                .AnyAsync(w => w.id == id, cancellationToken);
        }
    }
}
