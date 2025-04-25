using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository    
    {
        private readonly GymDbContext _context;
        public ClientRepository(GymDbContext context)
        {
            _context = context;
        }
        public async Task<Client> GetByIdAsync (int id, CancellationToken cancellationToken)
        {
            return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.id == id, cancellationToken);
        }
        public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Clients.AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task AddAsync (Client client, CancellationToken cancellationToken) 
        {
            await _context.Clients.AddAsync(client, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(Client client, CancellationToken cancellationToken) 
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync (int id, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Clients.AnyAsync(x => x.Email == email, cancellationToken);
        }
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Clients
                .AsNoTracking()
                .AnyAsync(c => c.id == id, cancellationToken);
        }
    }
}
