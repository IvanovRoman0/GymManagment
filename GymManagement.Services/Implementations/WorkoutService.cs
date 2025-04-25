using AutoMapper;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Core.Results;
using GymManagement.Infrastructure.Repositories;
using GymManagement.Services.Interfaces;
using GymManagment.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Services.Implementations
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IGymRepository _gymRepository;
        private readonly IMapper _mapper;

        public WorkoutService(
            IWorkoutRepository workoutRepository,
            IClientRepository clientRepository,
            IGymRepository gymRepository,
            IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _clientRepository = clientRepository;
            _gymRepository = gymRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<WorkoutDto>> CreateWorkoutAsync(WorkoutDto workoutDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _clientRepository.ExistsAsync(workoutDto.ClientId, cancellationToken))
                    return ServiceResult<WorkoutDto>.Failure("Клиент не найден", 404);

                if (!await _gymRepository.ExistsAsync(workoutDto.GymId, cancellationToken))
                    return ServiceResult<WorkoutDto>.Failure("Зал не найден", 404);

                var workout = Workout.Create(
                    workoutDto.ClientId,
                    workoutDto.WorkoutType,
                    workoutDto.Duration,
                    workoutDto.DateTime,
                    workoutDto.GymId);

                await _workoutRepository.AddAsync(workout, cancellationToken);
                workoutDto.Id = workout.id;
                return ServiceResult<WorkoutDto>.Success(workoutDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<WorkoutDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<WorkoutDto>> GetWorkoutByIdAsync(int id, CancellationToken cancellationToken)
        {
            var workout = await _workoutRepository.GetByIdAsync(id, cancellationToken);
            return workout == null
                ? ServiceResult<WorkoutDto>.Failure("Тренировка не найдена", 404)
                : ServiceResult<WorkoutDto>.Success(_mapper.Map<WorkoutDto>(workout));
        }

        public async Task<ServiceResult<IEnumerable<WorkoutDto>>> GetAllWorkoutsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var workouts = await _workoutRepository.GetAllAsync(cancellationToken);
                return ServiceResult<IEnumerable<WorkoutDto>>.Success(_mapper.Map<IEnumerable<WorkoutDto>>(workouts));
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<WorkoutDto>>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<WorkoutDto>> UpdateWorkoutAsync(int id, WorkoutDto workoutDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id != workoutDto.Id)
                    return ServiceResult<WorkoutDto>.Failure("ID тренировки не совпадает");

                var existing = await _workoutRepository.GetByIdAsync(id, cancellationToken);
                if (existing == null)
                    return ServiceResult<WorkoutDto>.Failure("Тренировка не найдена", 404);

                if (!await _clientRepository.ExistsAsync(workoutDto.ClientId, cancellationToken))
                    return ServiceResult<WorkoutDto>.Failure("Клиент не найден", 404);

                if (!await _gymRepository.ExistsAsync(workoutDto.GymId, cancellationToken))
                    return ServiceResult<WorkoutDto>.Failure("Зал не найден", 404);

                existing.ClientId = workoutDto.ClientId;
                existing.WorkoutType = workoutDto.WorkoutType;
                existing.Duration = workoutDto.Duration;
                existing.DateTime = workoutDto.DateTime;
                existing.GymId = workoutDto.GymId;

                await _workoutRepository.UpdateAsync(existing, cancellationToken);
                return ServiceResult<WorkoutDto>.Success(_mapper.Map<WorkoutDto>(existing));
            }
            catch (Exception ex)
            {
                return ServiceResult<WorkoutDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> DeleteWorkoutAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var workout = await _workoutRepository.GetByIdAsync(id, cancellationToken);
                if (workout == null)
                    return ServiceResult<bool>.Failure("Тренировка не найдена", 404);

                await _workoutRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}