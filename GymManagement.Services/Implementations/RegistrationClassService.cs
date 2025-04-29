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
    public class RegistrationClassService : IRegistrationClassService
    {
        private readonly IRegistrationClassRepository _registrationClassRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public RegistrationClassService(
            IRegistrationClassRepository registrationClassRepository,
            IClientRepository clientRepository,
            IClassRepository classRepository,
            IMapper mapper)
        {
            _registrationClassRepository = registrationClassRepository;
            _clientRepository = clientRepository;
            _classRepository = classRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<RegistrationClassDto>> CreateRegistrationAsync(RegistrationClassDto registrationclassDto, CancellationToken cancellationToken)
        {
                if (!await _clientRepository.ExistsAsync(registrationclassDto.ClientId, cancellationToken))
                    return ServiceResult<RegistrationClassDto>.Failure("Клиент не найден", 404);

                if (!await _classRepository.ExistsAsync(registrationclassDto.ClassId, cancellationToken))
                    return ServiceResult<RegistrationClassDto>.Failure("Занятие не найдено", 404);

                if (await _registrationClassRepository.ExistsRegistrationAsync(registrationclassDto.ClientId, registrationclassDto.ClassId, cancellationToken))
                    return ServiceResult<RegistrationClassDto>.Failure("Клиент уже зарегистрирован на это занятие");

                var registration = RegistrationClass.Create(
                    registrationclassDto.ClientId,
                    registrationclassDto.RegistrationDate,
                    registrationclassDto.ClassId);

                await _registrationClassRepository.AddAsync(registration, cancellationToken);
                registrationclassDto.Id = registration.id;
                return ServiceResult<RegistrationClassDto>.Success(registrationclassDto);
        }

        public async Task<ServiceResult<RegistrationClassDto>> GetRegistrationByIdAsync(int id, CancellationToken cancellationToken)
        {
            var registration = await _registrationClassRepository.GetByIdAsync(id, cancellationToken);
            return registration == null
                ? ServiceResult<RegistrationClassDto>.Failure("Регистрация не найдена", 404)
                : ServiceResult<RegistrationClassDto>.Success(_mapper.Map<RegistrationClassDto>(registration));
        }

        public async Task<ServiceResult<IEnumerable<RegistrationClassDto>>> GetAllRegistrationsAsync(CancellationToken cancellationToken)
        {
                var registrations = await _registrationClassRepository.GetAllAsync(cancellationToken);
                return ServiceResult<IEnumerable<RegistrationClassDto>>.Success(
                    _mapper.Map<IEnumerable<RegistrationClassDto>>(registrations));
        }

        public async Task<ServiceResult<RegistrationClassDto>> UpdateRegistrationAsync(int id, RegistrationClassDto registrationclassDto, CancellationToken cancellationToken)
        {
                if (id != registrationclassDto.Id)
                    return ServiceResult<RegistrationClassDto>.Failure("ID регистрации не совпадает");

                var existing = await _registrationClassRepository.GetByIdAsync(id, cancellationToken);
                if (existing == null)
                    return ServiceResult<RegistrationClassDto>.Failure("Регистрация не найдена", 404);

                if (!await _clientRepository.ExistsAsync(registrationclassDto.ClientId, cancellationToken))
                    return ServiceResult<RegistrationClassDto>.Failure("Клиент не найден", 404);

                if (!await _classRepository.ExistsAsync(registrationclassDto.ClassId, cancellationToken))
                    return ServiceResult<RegistrationClassDto>.Failure("Занятие не найдено", 404);

                existing.ClientId = registrationclassDto.ClientId;
                existing.RegistrationDate = registrationclassDto.RegistrationDate;
                existing.ClassId = registrationclassDto.ClassId;

                await _registrationClassRepository.UpdateAsync(existing, cancellationToken);
                return ServiceResult<RegistrationClassDto>.Success(_mapper.Map<RegistrationClassDto>(existing));
        }

        public async Task<ServiceResult<bool>> DeleteRegistrationAsync(int id, CancellationToken cancellationToken)
        {
                var registration = await _registrationClassRepository.GetByIdAsync(id, cancellationToken);
                if (registration == null)
                    return ServiceResult<bool>.Failure("Регистрация не найдена", 404);

                await _registrationClassRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
        }
    }
}
