using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface IGymRepository
    {
        Task<Gym> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Gym>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Gym gym, CancellationToken cancellationToken = default);
        Task UpdateAsync(Gym gym, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default);
    }
}
