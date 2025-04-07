using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Results;

namespace GymManagement.Services.Interfaces
{
    public interface IMembershipService
    {
        Task<ServiceResult<MembershipDto>> CreateMembershipAsync(MembershipDto membershipDto);
        Task<ServiceResult<MembershipDto>> GetMembershipByIdAsync(int id);
        Task<ServiceResult<MembershipDto>> UpdateMembershipAsync(int id, MembershipDto membershipDto);
        Task<ServiceResult<bool>> DeleteMembershipAsync(int id);
        Task<ServiceResult<bool>> MembershipTypeExistsAsync(string membershipType);
    }
}
