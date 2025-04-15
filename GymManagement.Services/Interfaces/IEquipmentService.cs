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
    public interface IEquipmentService
    {
        Task<ServiceResult<EquipmentDto>> CreateEquipmentAsync(EquipmentDto equipmentDto, CancellationToken cancellationToken);
        Task<ServiceResult<EquipmentDto>> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<EquipmentDto>>> GetAllEquipmentAsync(CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<EquipmentDto>>> GetEquipmentByGymIdAsync(int gymId, CancellationToken cancellationToken);
        Task<ServiceResult<EquipmentDto>> UpdateEquipmentAsync(int id, EquipmentDto equipmentDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteEquipmentAsync(int id, CancellationToken cancellationToken);
    }
}
