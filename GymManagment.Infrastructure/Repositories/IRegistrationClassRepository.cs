using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface IRegistrationClassRepository
    {
        Task<RegistrationClass> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<RegistrationClass>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(RegistrationClass registrationClass, CancellationToken cancellationToken = default);
        Task UpdateAsync(RegistrationClass registrationClass, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsRegistrationAsync(int clientId, int classId, CancellationToken cancellationToken = default);
    }
}
