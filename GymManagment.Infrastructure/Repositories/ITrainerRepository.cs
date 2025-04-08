using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface ITrainerRepository
    {
        Task<Trainer> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Trainer>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Trainer trainer, CancellationToken cancellationToken = default);
        Task UpdateAsync(Trainer trainer, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    }
}
