using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Core.Results;

namespace GymManagement.Services.Interfaces
{
    public interface IClientService
    {
        Task<ServiceResult<ClientDto>> CreateClientAsync(ClientDto clientDto);
        Task<ServiceResult<ClientDto>> GetClientByIdAsync(int id);
        Task<ServiceResult<IEnumerable<ClientDto>>> GetAllClientAsync();
        Task<ServiceResult<ClientDto>> UpdateClientAsync(int id, ClientDto clientDto);
        Task<ServiceResult<bool>> DeleteClientAsync(int id);
        Task<ServiceResult<bool>> EmailExistsAsync(string email);

    }
}
