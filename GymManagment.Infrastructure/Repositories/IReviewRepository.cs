using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface IReviewRepository
    {
        Task<Reviews> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Reviews>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Reviews reviews, CancellationToken cancellationToken = default);
        Task UpdateAsync(Reviews reviews, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
