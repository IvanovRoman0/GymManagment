using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly GymDbContext _context;
        public MembershipRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<Membership> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Memberships.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Membership>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Memberships.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Membership membership, CancellationToken cancellationToken = default)
        {
            await _context.Memberships.AddAsync(membership, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var membership = await _context.Memberships.FindAsync(id);
            if (membership != null)
            {
                _context.Memberships.Remove(membership);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(Membership membership, CancellationToken cancellationToken = default)
        {
            _context.Entry(membership).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsByTypeAsync(string membershiptype, CancellationToken cancellationToken = default)
        {
            return await _context.Memberships.AnyAsync(m => m.MembershipType ==  membershiptype, cancellationToken);
        }
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Memberships
                .AnyAsync(m => m.Id == id, cancellationToken);
        }
    }
}
