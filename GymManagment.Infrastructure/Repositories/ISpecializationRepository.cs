using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface ISpecializationRepository
    {
        Task<Specialization> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Specialization>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync (Specialization specialization, CancellationToken cancellationToken = default);
        Task UpdateAsync(Specialization specialization, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id , CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id , CancellationToken cancellationToken = default);
        Task<bool> NameExistsAsync(string specializationName, CancellationToken cancellationToken = default);
    }
}
