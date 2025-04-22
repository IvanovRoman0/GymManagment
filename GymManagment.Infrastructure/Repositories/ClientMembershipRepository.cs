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
    public class ClientMembershipRepository : IClientMembershipRepository
    {
        private readonly GymDbContext _context;

        public ClientMembershipRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<ClientMembership> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.ClientMemberships
                .Include(cm => cm.Client)
                .Include(cm => cm.Membership)
                .AsNoTracking()
                .FirstOrDefaultAsync(cm => cm.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<ClientMembership>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.ClientMemberships
                .Include(cm => cm.Client)
                .Include(cm => cm.Membership)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(ClientMembership clientMembership, CancellationToken cancellationToken)
        {
            await _context.ClientMemberships.AddAsync(clientMembership, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ClientMembership clientMembership, CancellationToken cancellationToken)
        {
            _context.Entry(clientMembership).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var clientMembership = await _context.ClientMemberships.FindAsync(id);
            if (clientMembership != null)
            {
                _context.ClientMemberships.Remove(clientMembership);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.ClientMemberships
                .AnyAsync(cm => cm.Id == id, cancellationToken);
        }
    }
}
