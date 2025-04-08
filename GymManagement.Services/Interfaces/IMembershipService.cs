using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Results;

namespace GymManagement.Services.Interfaces
{
    public interface IMembershipService
    {
        Task<ServiceResult<MembershipDto>> CreateMembershipAsync(MembershipDto membershipDto, CancellationToken cancellationToken = default);
        Task<ServiceResult<MembershipDto>> GetMembershipByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ServiceResult<MembershipDto>> UpdateMembershipAsync(int id, MembershipDto membershipDto, CancellationToken cancellationToken = default);
        Task<ServiceResult<bool>> DeleteMembershipAsync(int id, CancellationToken cancellationToken = default);
        Task<ServiceResult<bool>> MembershipTypeExistsAsync(string membershipType, CancellationToken cancellationToken = default);
    }
}
