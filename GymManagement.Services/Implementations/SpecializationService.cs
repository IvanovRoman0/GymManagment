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
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IMapper _mapper;
        public SpecializationService(ISpecializationRepository specializationRepository, IMapper mapper)
        {
            _specializationRepository = specializationRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<SpecializationDto>> CreateSpecializationAsync(SpecializationDto specializationDto, CancellationToken cancellationToken)
        {
            try
            {
                if (await _specializationRepository.NameExistsAsync(specializationDto.SpecializationName, cancellationToken))
                    return ServiceResult<SpecializationDto>.Failure("Специализация с таким названием уже существут");
                var specialization = Specialization.Create(specializationDto.SpecializationName);
                await _specializationRepository.AddAsync(specialization, cancellationToken);
                specializationDto.Id = specialization.Id;
                return ServiceResult<SpecializationDto>.Success(specializationDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SpecializationDto>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<SpecializationDto>> GetSpecializationByIdAsync(int id, CancellationToken cancellationToken)
        {
            var specialization = await _specializationRepository.GetByIdAsync(id, cancellationToken);
            return specialization == null ? ServiceResult<SpecializationDto>.Failure("Специализация не найдена", 404)
                : ServiceResult<SpecializationDto>.Success(_mapper.Map<SpecializationDto>(specialization));
        }
        public async Task<ServiceResult<IEnumerable<SpecializationDto>>> GetAllSpecializationAsync(CancellationToken cancellationToken)
        {
            try
            {
                var specialization = await _specializationRepository.GetAllAsync(cancellationToken);
                var specializationDtos = _mapper.Map<IEnumerable<SpecializationDto>>(specialization);
                return ServiceResult<IEnumerable<SpecializationDto>>.Success(specializationDtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<SpecializationDto>>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<SpecializationDto>> UpdateSpecializationAsync(int id, SpecializationDto specializationDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id != specializationDto.Id)
                    return ServiceResult<SpecializationDto>.Failure("ID специализации не совпадает");

                var existing = await _specializationRepository.GetByIdAsync(id, cancellationToken);
                if (existing == null)
                    return ServiceResult<SpecializationDto>.Failure("Специализация не найдена", 404);

                if (!string.Equals(existing.SpecializationName, specializationDto.SpecializationName, StringComparison.OrdinalIgnoreCase) &&
                    await _specializationRepository.NameExistsAsync(specializationDto.SpecializationName, cancellationToken))
                {
                    return ServiceResult<SpecializationDto>.Failure("Специализация с таким названием уже существует");
                }

                existing.SpecializationName = specializationDto.SpecializationName;
                await _specializationRepository.UpdateAsync(existing, cancellationToken);
                return ServiceResult<SpecializationDto>.Success(_mapper.Map<SpecializationDto>(existing));
            }
            catch (Exception ex)
            {
                return ServiceResult<SpecializationDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> DeleteSpecializationAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var specialization = await _specializationRepository.GetByIdAsync(id, cancellationToken);
                if (specialization == null)
                    return ServiceResult<bool>.Failure("Специализация не найдена", 404);
                await _specializationRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
