using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GymManagment.Infrastructure.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly GymDbContext _context;
        public MembershipRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<Membership> GetByIdAsync(int id)
        {
            return await _context.Memberships.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Membership>> GetAllAsync()
        {
            return await _context.Memberships.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Membership membership)
        {
            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);
            if (membership != null)
            {
                _context.Memberships.Remove(membership);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Membership membership)
        {
            _context.Entry(membership).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByTypeAsync(string membershiptype)
        {
            return await _context.Memberships.AnyAsync(m => m.MembershipType ==  membershiptype);
        }
    }
}
