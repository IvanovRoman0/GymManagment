using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface IEquipmentRepository
    {
        Task<Equipment> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Equipment>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Equipment>> GetByGymIdAsync(int gymId, CancellationToken cancellationToken = default);
        Task AddAsync(Equipment equipment, CancellationToken cancellationToken = default);
        Task UpdateAsync(Equipment equipment, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> NameExistsInGymAsync(string name, int gymId, CancellationToken cancellationToken = default);
    }
}
