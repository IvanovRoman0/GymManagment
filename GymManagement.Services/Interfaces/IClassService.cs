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
    public interface IClassService
    {
        Task<ServiceResult<ClassDto>> CreateClassAsync(ClassDto classDto, CancellationToken cancellationToken);
        Task<ServiceResult<ClassDto>> GetClassByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<ClassDto>>> GetAllClassesAsync(CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<ClassDto>>> GetClassesByGymIdAsync(int gymId, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<ClassDto>>> GetClassesByTrainerIdAsync(int trainerId, CancellationToken cancellationToken);
        Task<ServiceResult<ClassDto>> UpdateClassAsync(int id, ClassDto classDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteClassAsync(int id, CancellationToken cancellationToken);
    }
}
