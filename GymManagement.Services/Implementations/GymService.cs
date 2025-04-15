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
    public class GymService : IGymService
    {
        private readonly IGymRepository _gymRepository;
        private readonly IMapper _mapper;

        public GymService(IGymRepository gymRepository, IMapper mapper)
        {
            _gymRepository = gymRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<GymDto>> CreateGymAsync(GymDto gymDto, CancellationToken cancellationToken)
        {
            try
            {
                if (await _gymRepository.NameExistsAsync(gymDto.GymName, cancellationToken))
                    return ServiceResult<GymDto>.Failure("Зал с таким названием уже существует");

                var gym = Gym.Create(gymDto.GymName, gymDto.Location);
                await _gymRepository.AddAsync(gym, cancellationToken);
                gymDto.Id = gym.Id;
                return ServiceResult<GymDto>.Success(gymDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<GymDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<GymDto>> GetGymByIdAsync(int id, CancellationToken cancellationToken)
        {
            var gym = await _gymRepository.GetByIdAsync(id, cancellationToken);
            return gym == null
                ? ServiceResult<GymDto>.Failure("Зал не найден", 404)
                : ServiceResult<GymDto>.Success(_mapper.Map<GymDto>(gym));
        }

        public async Task<ServiceResult<IEnumerable<GymDto>>> GetAllGymsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var gyms = await _gymRepository.GetAllAsync(cancellationToken);
                var gymDtos = _mapper.Map<IEnumerable<GymDto>>(gyms);
                return ServiceResult<IEnumerable<GymDto>>.Success(gymDtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<GymDto>>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<GymDto>> UpdateGymAsync(int id, GymDto gymDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id != gymDto.Id)
                    return ServiceResult<GymDto>.Failure("ID зала не совпадает");

                var existing = await _gymRepository.GetByIdAsync(id, cancellationToken);
                if (existing == null)
                    return ServiceResult<GymDto>.Failure("Зал не найден", 404);

                if (!string.Equals(existing.GymName, gymDto.GymName, StringComparison.OrdinalIgnoreCase) &&
                    await _gymRepository.NameExistsAsync(gymDto.GymName, cancellationToken))
                {
                    return ServiceResult<GymDto>.Failure("Зал с таким названием уже существует");
                }

                existing.GymName = gymDto.GymName;
                existing.Location = gymDto.Location;
                await _gymRepository.UpdateAsync(existing, cancellationToken);
                return ServiceResult<GymDto>.Success(_mapper.Map<GymDto>(existing));
            }
            catch (Exception ex)
            {
                return ServiceResult<GymDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> DeleteGymAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var gym = await _gymRepository.GetByIdAsync(id, cancellationToken);
                if (gym == null)
                    return ServiceResult<bool>.Failure("Зал не найден", 404);

                await _gymRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
