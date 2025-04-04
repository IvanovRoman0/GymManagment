using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;

namespace GymManagement.Services.Interfaces
{
    public interface IMembershipService
    {
        Task<MembershipDto> GetByIdAsync(int id);
        Task<IEnumerable<MembershipDto>> GetAllAsync();
        Task AddAsync(MembershipDto membershipDto);
        Task UpdateAsync(int id, MembershipDto membershipDto);
        Task DeleteAsync(int id);
    }
}
