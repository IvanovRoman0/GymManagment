using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface IClassRepository
    {
        Task<Class> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Class>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Class classEntity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Class classEntity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
