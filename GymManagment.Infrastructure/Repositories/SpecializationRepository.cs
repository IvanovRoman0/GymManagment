using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;


namespace GymManagment.Infrastructure.Repositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly GymDbContext _context;
        public SpecializationRepository(GymDbContext context)
        {
            _context = context;
        }
        public async Task<Specialization> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Specializations
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<Specialization>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Specializations
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task AddAsync(Specialization specialization, CancellationToken cancellationToken)
        {
            await _context.Specializations.AddAsync(specialization, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(Specialization specialization, CancellationToken cancellationToken)
        {
            _context.Entry(specialization).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var specialization = await _context.Specializations.FindAsync(id);
            if (specialization != null)
            {
                _context.Specializations.Remove(specialization);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Specializations
                .AnyAsync(s => s.Id == id, cancellationToken);
        }
        public async Task<bool> NameExistsAsync(string specializationName, CancellationToken cancellationToken)
        {
            return await _context.Specializations
                .AnyAsync(s => s.SpecializationName == specializationName, cancellationToken);
        }
    }
}
