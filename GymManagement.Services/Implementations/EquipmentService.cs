using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Core.Results;
using GymManagement.Services.Interfaces;
using GymManagment.Infrastructure.Repositories;

namespace GymManagement.Services.Implementations
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IGymRepository _gymRepository;
        private readonly IMapper _mapper;

        public EquipmentService(
            IEquipmentRepository equipmentRepository,
            IGymRepository gymRepository,
            IMapper mapper)
        {
            _equipmentRepository = equipmentRepository;
            _gymRepository = gymRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<EquipmentDto>> CreateEquipmentAsync(EquipmentDto equipmentDto, CancellationToken cancellationToken)
        {
                if (!await _gymRepository.ExistsAsync(equipmentDto.GymId, cancellationToken))
                    return ServiceResult<EquipmentDto>.Failure("Зал не найден", 404);

                if (await _equipmentRepository.NameExistsInGymAsync(equipmentDto.EquipmentName, equipmentDto.GymId, cancellationToken))
                    return ServiceResult<EquipmentDto>.Failure("Оборудование с таким названием уже существует в этом зале");

                var equipment = Equipment.Create(equipmentDto.EquipmentName, equipmentDto.GymId, equipmentDto.DatePurchase);
                await _equipmentRepository.AddAsync(equipment, cancellationToken);
                equipmentDto.Id = equipment.id;
                return ServiceResult<EquipmentDto>.Success(equipmentDto);
        }

        public async Task<ServiceResult<EquipmentDto>> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id, cancellationToken);
            return equipment == null
                ? ServiceResult<EquipmentDto>.Failure("Оборудование не найдено", 404)
                : ServiceResult<EquipmentDto>.Success(_mapper.Map<EquipmentDto>(equipment));
        }

        public async Task<ServiceResult<IEnumerable<EquipmentDto>>> GetAllEquipmentAsync(CancellationToken cancellationToken)
        {
                var equipment = await _equipmentRepository.GetAllAsync(cancellationToken);
                return ServiceResult<IEnumerable<EquipmentDto>>.Success(_mapper.Map<IEnumerable<EquipmentDto>>(equipment));
        }

        public async Task<ServiceResult<EquipmentDto>> UpdateEquipmentAsync(int id, EquipmentDto equipmentDto, CancellationToken cancellationToken)
        {
                if (id != equipmentDto.Id)
                    return ServiceResult<EquipmentDto>.Failure("ID оборудования не совпадает");

                var existing = await _equipmentRepository.GetByIdAsync(id, cancellationToken);
                if (existing == null)
                    return ServiceResult<EquipmentDto>.Failure("Оборудование не найдено", 404);

                if (existing.GymId != equipmentDto.GymId)
                    return ServiceResult<EquipmentDto>.Failure("Нельзя изменить зал для оборудования");

                if (!string.Equals(existing.EquipmentName, equipmentDto.EquipmentName, StringComparison.OrdinalIgnoreCase) &&
                    await _equipmentRepository.NameExistsInGymAsync(equipmentDto.EquipmentName, equipmentDto.GymId, cancellationToken))
                {
                    return ServiceResult<EquipmentDto>.Failure("Оборудование с таким названием уже существует в этом зале");
                }

                existing.EquipmentName = equipmentDto.EquipmentName;
                existing.DatePurchase = equipmentDto.DatePurchase;

                await _equipmentRepository.UpdateAsync(existing, cancellationToken);
                return ServiceResult<EquipmentDto>.Success(_mapper.Map<EquipmentDto>(existing));
        }

        public async Task<ServiceResult<bool>> DeleteEquipmentAsync(int id, CancellationToken cancellationToken)
        {
                var equipment = await _equipmentRepository.GetByIdAsync(id, cancellationToken);
                if (equipment == null)
                    return ServiceResult<bool>.Failure("Оборудование не найдено", 404);

                await _equipmentRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
        }
    }
}
