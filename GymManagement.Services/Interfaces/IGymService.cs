using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Results;

namespace GymManagement.Services.Interfaces
{
    public interface IGymService
    {
        Task<ServiceResult<GymDto>> CreateGymAsync(GymDto gymDto, CancellationToken cancellationToken);
        Task<ServiceResult<GymDto>> GetGymByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<GymDto>>> GetAllGymsAsync(CancellationToken cancellationToken);
        Task<ServiceResult<GymDto>> UpdateGymAsync(int id, GymDto gymDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteGymAsync(int id, CancellationToken cancellationToken);
    }
}
