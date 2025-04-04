using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;

namespace GymManagement.Services.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto> GetByIdAsync(int id);
        Task<IEnumerable<ClientDto>> GetAllAsync();
        Task AddAsync (ClientDto clientDto);
        Task UpdateAsync (int id, ClientDto clientDto);
        Task DeleteAsync (int id);
    }
}
