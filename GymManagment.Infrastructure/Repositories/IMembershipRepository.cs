using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagement.Infrastructure.Repositories
{
    public interface IMembershipRepository
    {
        Task<Membership> GetByIdAsync(int id);
        Task<IEnumerable<Membership>> GetAllAsync();
        Task AddAsync(Membership membership);
        Task UpdateAsync(Membership membership);
        Task DeleteAsync(int id);
        Task<bool> ExistsByTypeAsync(string membershiptype);

    }
}
