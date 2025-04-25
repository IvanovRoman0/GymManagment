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
using GymManagement.Infrastructure.Repositories;
using GymManagement.Services.Interfaces;
using GymManagment.Infrastructure.Repositories;

namespace GymManagement.Services.Implementations
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;
        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<TrainerDto>> CreateTrainerAsync(TrainerDto trainerDto, CancellationToken cancellationToken)
        {
            try
            {
                if (await _trainerRepository.EmailExistsAsync(trainerDto.Email, cancellationToken))
                    return ServiceResult<TrainerDto>.Failure("Email уже существует");
                var trainer = Trainer.Create(
                    trainerDto.FirstName,
                    trainerDto.LastName,
                    trainerDto.PhoneNumber,
                    trainerDto.Email,
                    trainerDto.SpecializationId);
                await _trainerRepository.AddAsync(trainer, cancellationToken);
                trainerDto.Id = trainer.id;
                return ServiceResult<TrainerDto>.Success(trainerDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<TrainerDto>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<TrainerDto>> GetTrainerByIdAsync(int id, CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken);
            return trainer == null ? ServiceResult<TrainerDto>.Failure("Тренер не найден", 404)
                : ServiceResult<TrainerDto>.Success(_mapper.Map<TrainerDto>(trainer));
        }
        public async Task<ServiceResult<IEnumerable<TrainerDto>>> GetAllTrainerAsync(CancellationToken cancellationToken)
        {
            try
            {
                var trainer = await _trainerRepository.GetAllAsync(cancellationToken);
                var trainerDtos = _mapper.Map<IEnumerable<TrainerDto>>(trainer);
                return ServiceResult<IEnumerable<TrainerDto>>.Success(trainerDtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<TrainerDto>>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<TrainerDto>> UpdateTrainerAsync(int id, TrainerDto trainer, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _trainerRepository.ExistsAsync(id, cancellationToken))
                    return ServiceResult<TrainerDto>.Failure("Тренер не найден", 404);
                if (id != trainer.Id)
                    return ServiceResult<TrainerDto>.Failure("ID тренера не совпадает");
                var existingTrainer = await _trainerRepository.GetByIdAsync(id, cancellationToken);
                if (existingTrainer == null)
                    return ServiceResult<TrainerDto>.Failure("Тренер не найден", 404);
                if (!string.Equals(existingTrainer.Email, trainer.Email, StringComparison.OrdinalIgnoreCase)
                    && await _trainerRepository.EmailExistsAsync(trainer.Email))
                {
                    return ServiceResult<TrainerDto>.Failure("Новый Email уже занят другим пользователем");
                }
                existingTrainer.FirstName = trainer.FirstName;
                existingTrainer.LastName = trainer.LastName;
                existingTrainer.Email = trainer.Email;
                existingTrainer.PhoneNumber = trainer.PhoneNumber;
                existingTrainer.SpecializationId = trainer.SpecializationId;
                await _trainerRepository.UpdateAsync(existingTrainer, cancellationToken);
                return ServiceResult<TrainerDto>.Success(_mapper.Map<TrainerDto>(existingTrainer));
            }
            catch (Exception ex)
            {
                return ServiceResult<TrainerDto>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> DeleteTrainerAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var trainer = await _trainerRepository.GetByIdAsync(id, cancellationToken);
                if (trainer == null)
                    return ServiceResult<bool>.Failure("тренер не найден", 404);
                await _trainerRepository.DeleteAsync(id);
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await _trainerRepository.EmailExistsAsync(email, cancellationToken);
                return ServiceResult<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
