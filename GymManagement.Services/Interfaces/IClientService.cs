using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Results;

namespace GymManagement.Services.Interfaces
{
    public interface IClientService
    {
        Task<ServiceResult<ClientDto>> CreateClientAsync(ClientDto clientDto, CancellationToken cancellationToken);
        Task<ServiceResult<ClientDto>> GetClientByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<ClientDto>>> GetAllClientAsync(CancellationToken cancellationToken);
        Task<ServiceResult<ClientDto>> UpdateClientAsync(int id, ClientDto clientDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteClientAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> EmailExistsAsync(string email, CancellationToken cancellationToken);
    }
}
