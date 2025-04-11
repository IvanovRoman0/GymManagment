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
    public interface ISpecializationService
    {
        Task <ServiceResult<SpecializationDto>> CreateSpecializationAsync(SpecializationDto specioalizationDto, CancellationToken cancellationToken);
        Task <ServiceResult<SpecializationDto>> GetSpecializationByIdAsync(int id, CancellationToken cancellationToken);
        Task <ServiceResult<IEnumerable<SpecializationDto>>> GetAllSpecializationAsync(CancellationToken cancellationToken);
        Task<ServiceResult<SpecializationDto>> UpdateSpecializationAsync(int id, SpecializationDto specioalizationDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteSpecializationAsync(int id, CancellationToken cancellationToken);
    }
}
