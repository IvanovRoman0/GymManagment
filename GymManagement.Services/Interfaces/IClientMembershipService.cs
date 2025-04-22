using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Results;

namespace GymManagement.Services.Interfaces
{
    public interface IClientMembershipService
    {
        Task<ServiceResult<ClientMembershipDto>> CreateClientMembershipAsync(ClientMembershipDto clientmembershipDto, CancellationToken cancellationToken);
        Task<ServiceResult<ClientMembershipDto>> GetClientMembershipByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<ClientMembershipDto>>> GetAllClientMembershipsAsync(CancellationToken cancellationToken);
        Task<ServiceResult<ClientMembershipDto>> UpdateClientMembershipAsync(int id, ClientMembershipDto clientmembershipDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteClientMembershipAsync(int id, CancellationToken cancellationToken);
    }
}
