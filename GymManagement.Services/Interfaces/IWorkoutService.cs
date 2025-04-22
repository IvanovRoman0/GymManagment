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
    public interface IWorkoutService
    {
        Task<ServiceResult<WorkoutDto>> CreateWorkoutAsync(WorkoutDto workoutDto, CancellationToken cancellationToken);
        Task<ServiceResult<WorkoutDto>> GetWorkoutByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<WorkoutDto>>> GetAllWorkoutsAsync(CancellationToken cancellationToken);
        Task<ServiceResult<WorkoutDto>> UpdateWorkoutAsync(int id, WorkoutDto workoutDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteWorkoutAsync(int id, CancellationToken cancellationToken);
    }
}
