using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface IClientMembershipRepository
    {
        Task<ClientMembership> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ClientMembership>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(ClientMembership clientMembership, CancellationToken cancellationToken = default);
        Task UpdateAsync(ClientMembership clientMembership, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
