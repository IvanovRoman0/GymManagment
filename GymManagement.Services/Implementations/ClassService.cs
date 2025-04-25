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
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IGymRepository _gymRepository;
        private readonly IMapper _mapper;

        public ClassService(
            IClassRepository classRepository,
            ITrainerRepository trainerRepository,
            IGymRepository gymRepository,
            IMapper mapper)
        {
            _classRepository = classRepository;
            _trainerRepository = trainerRepository;
            _gymRepository = gymRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ClassDto>> CreateClassAsync(ClassDto classDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _gymRepository.ExistsAsync(classDto.GymId, cancellationToken))
                    return ServiceResult<ClassDto>.Failure("Зал не найден", 404);

                if (classDto.TrainerId.HasValue &&
                    !await _trainerRepository.ExistsAsync(classDto.TrainerId.Value, cancellationToken))
                    return ServiceResult<ClassDto>.Failure("Тренер не найден", 404);

                var classEntity = Class.Create(classDto.GymId, classDto.ClassName, classDto.ClassType, classDto.TrainerId);
                if (classDto.DateTime != default)
                {
                    classEntity.DateTime = classDto.DateTime;
                }

                await _classRepository.AddAsync(classEntity, cancellationToken);
                classDto.Id = classEntity.id;
                return ServiceResult<ClassDto>.Success(classDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<ClassDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ClassDto>>> GetAllClassesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var classes = await _classRepository.GetAllAsync(cancellationToken);
                return ServiceResult<IEnumerable<ClassDto>>.Success(_mapper.Map<IEnumerable<ClassDto>>(classes));
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ClassDto>>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<ClassDto>> UpdateClassAsync(int id, ClassDto classDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id != classDto.Id)
                    return ServiceResult<ClassDto>.Failure("ID занятия не совпадает");

                var existing = await _classRepository.GetByIdAsync(id, cancellationToken);
                if (existing == null)
                    return ServiceResult<ClassDto>.Failure("Занятие не найдено", 404);

                if (!await _gymRepository.ExistsAsync(classDto.GymId, cancellationToken))
                    return ServiceResult<ClassDto>.Failure("Зал не найден", 404);

                if (classDto.TrainerId.HasValue &&
                    !await _trainerRepository.ExistsAsync(classDto.TrainerId.Value, cancellationToken))
                    return ServiceResult<ClassDto>.Failure("Тренер не найден", 404);

                existing.GymId = classDto.GymId;
                existing.ClassName = classDto.ClassName;
                existing.ClassType = classDto.ClassType;
                existing.TrainerId = classDto.TrainerId;
                existing.DateTime = classDto.DateTime;

                await _classRepository.UpdateAsync(existing, cancellationToken);
                return ServiceResult<ClassDto>.Success(_mapper.Map<ClassDto>(existing));
            }
            catch (Exception ex)
            {
                return ServiceResult<ClassDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> DeleteClassAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var classEntity = await _classRepository.GetByIdAsync(id, cancellationToken);
                if (classEntity == null)
                    return ServiceResult<bool>.Failure("Занятие не найдено", 404);

                await _classRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
