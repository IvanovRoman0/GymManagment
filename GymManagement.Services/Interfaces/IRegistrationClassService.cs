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
    public interface IRegistrationClassService
    {
        Task<ServiceResult<RegistrationClassDto>> CreateRegistrationAsync(RegistrationClassDto registrationclassDto, CancellationToken cancellationToken);
        Task<ServiceResult<RegistrationClassDto>> GetRegistrationByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<RegistrationClassDto>>> GetAllRegistrationsAsync(CancellationToken cancellationToken);
        Task<ServiceResult<RegistrationClassDto>> UpdateRegistrationAsync(int id, RegistrationClassDto registrationclassDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteRegistrationAsync(int id, CancellationToken cancellationToken);
    }
}
