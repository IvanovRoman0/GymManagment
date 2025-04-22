using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface IWorkoutRepository
    {
        Task<Workout> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Workout>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Workout workout, CancellationToken cancellationToken = default);
        Task UpdateAsync(Workout workout, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
