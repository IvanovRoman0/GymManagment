using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymManagement.Services.Interfaces;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.Repositories;
using AutoMapper;
using System.Threading;
using GymManagement.Core.Results;

namespace GymManagement.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<ClientDto>> CreateClientAsync(ClientDto clientDto, CancellationToken cancellationToken)
        {
            try
            {
                if (await _clientRepository.EmailExistsAsync(clientDto.Email, cancellationToken))
                    return ServiceResult<ClientDto>.Failure("Email уже существует");
                var client = Client.Create(
                    clientDto.FirstName,
                    clientDto.LastName,
                    clientDto.PhoneNumber,
                    clientDto.Email,
                    clientDto.DateOfBirth,
                    clientDto.Gender);
                await _clientRepository.AddAsync(client, cancellationToken);
                clientDto.Id = client.Id;
                return ServiceResult<ClientDto>.Success(clientDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<ClientDto>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<ClientDto>> GetClientByIdAsync(int id, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(id, cancellationToken);
            return client == null ? ServiceResult<ClientDto>.Failure("Клиент не найден", 404)
                : ServiceResult<ClientDto>.Success(_mapper.Map<ClientDto>(client));
        }
        public async Task<ServiceResult<IEnumerable<ClientDto>>> GetAllClientAsync(CancellationToken cancellationToken)
        {
            try
            {
                var clients = await _clientRepository.GetAllAsync(cancellationToken);
                var clientDtos = _mapper.Map<IEnumerable<ClientDto>>(clients);
                return ServiceResult<IEnumerable<ClientDto>>.Success(clientDtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ClientDto>>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<ClientDto>> UpdateClientAsync(int id, ClientDto client, CancellationToken cancellationToken)
        {
            try
            {
                if (id != client.Id)
                    return ServiceResult<ClientDto>.Failure("ID клиента не совпадает");
                var existingClient = await _clientRepository.GetByIdAsync(id, cancellationToken);
                if (existingClient == null)
                    return ServiceResult<ClientDto>.Failure("Клиент не найден", 404);
                if (!string.Equals(existingClient.Email, client.Email, StringComparison.OrdinalIgnoreCase) 
                    && await _clientRepository.EmailExistsAsync(client.Email))
                {
                    return ServiceResult<ClientDto>.Failure("Новый email уже занят другим пользователем");
                }
                existingClient.FirstName = client.FirstName;
                existingClient.LastName = client.LastName;
                existingClient.Email = client.Email;
                existingClient.PhoneNumber = client.PhoneNumber;
                existingClient.DateOfBirth = client.DateOfBirth;
                existingClient.Gender = client.Gender;
                await _clientRepository.UpdateAsync(existingClient, cancellationToken);
                return ServiceResult<ClientDto>.Success(_mapper.Map<ClientDto>(existingClient));
            }
            catch (Exception ex)
            {
                return ServiceResult<ClientDto>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> DeleteClientAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id, cancellationToken);
                if (client == null)
                    return ServiceResult<bool>.Failure("Клиент не найден", 404);
                await _clientRepository.DeleteAsync(id);
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
                var exists = await _clientRepository.EmailExistsAsync(email, cancellationToken);
                return ServiceResult<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
