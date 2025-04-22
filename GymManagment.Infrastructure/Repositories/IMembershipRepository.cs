using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagement.Infrastructure.Repositories
{
    public interface IMembershipRepository
    {
        Task<Membership> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Membership>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Membership membership, CancellationToken cancellationToken = default);
        Task UpdateAsync(Membership membership, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByTypeAsync(string membershiptype, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

    }
}
