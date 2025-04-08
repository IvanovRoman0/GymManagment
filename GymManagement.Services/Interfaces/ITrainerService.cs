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
    public interface ITrainerService
    {
        Task<ServiceResult<TrainerDto>> CreateTrainerAsync(TrainerDto trainerDto , CancellationToken cancellationToken);
        Task<ServiceResult<TrainerDto>> GetTrainerByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<TrainerDto>>> GetAllTrainerAsync(CancellationToken cancellationToken);
        Task<ServiceResult<TrainerDto>> UpdateTrainerAsync(int id, TrainerDto trainerDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteTrainerAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> EmailExistsAsync(string email, CancellationToken cancellationToken);
    }
}
