using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<Client> GetByIdAsync (int id)
        {
            return await _context.Clients.FindAsync(id);
        }
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }
        public async Task AddAsync (Client client) 
        {
            if (await EmailExistsAsync(client.Email))
                throw new InvalidOperationException("электронная почта уже существует");
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Client client) 
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync (int id)
        {
            var client = await GetByIdAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Clients.AnyAsync(x => x.Email == email);
        }
    }
}
