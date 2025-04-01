using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagement.Services.Interfaces
{
    public interface IClientService
    {
        Task<Client> GetByIdAsync(int id);
        Task<IEnumerable<Client>> GetAllAsync();
        Task AddAsync (Client client);
        Task UpdateAsync (int id, Client client);
        Task DeleteAsync (int id);
    }
}
