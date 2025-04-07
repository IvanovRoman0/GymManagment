using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<ServiceResult<ClientDto>> CreateClientAsync(ClientDto clientDto)
        {
            try
            {
                if (await _clientRepository.EmailExistsAsync(clientDto.Email))
                    return ServiceResult<ClientDto>.Failure("Email уже существует");
                var client = Client.Create(
                    clientDto.FirstName,
                    clientDto.LastName,
                    clientDto.PhoneNumber,
                    clientDto.Email,
                    clientDto.DateOfBirth,
                    clientDto.Gender);
                await _clientRepository.AddAsync(client);
                clientDto.Id = client.Id;
                return ServiceResult<ClientDto>.Success(clientDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<ClientDto>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<ClientDto>> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            return client == null ? ServiceResult<ClientDto>.Failure("Клиент не найден")
                : ServiceResult<ClientDto>.Success(_mapper.Map<ClientDto>(client));
        }
        public async Task<ServiceResult<IEnumerable<ClientDto>>> GetAllClientAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAllAsync();
                var clientDtos = _mapper.Map<IEnumerable<ClientDto>>(clients);
                return ServiceResult<IEnumerable<ClientDto>>.Success(clientDtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ClientDto>>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<ClientDto>> UpdateClientAsync(int id, ClientDto client)
        {
            try
            {
                if (id != client.Id)
                    return ServiceResult<ClientDto>.Failure("ID клиента не совпадает");
                var existingClient = await _clientRepository.GetByIdAsync(id);
                if (existingClient == null)
                    return ServiceResult<ClientDto>.Failure("Клиент не найден");
                if (!string.Equals(existingClient.Email, client.Email, StringComparison.OrdinalIgnoreCase) 
                    && await _clientRepository.EmailExistsAsync(client.Email))
                {
                    return ServiceResult<ClientDto>.Failure("Новый email уже занят другим пользователем");
                }
                existingClient.UpdatePersonalInfo(
                    client.FirstName,
                    client.LastName,
                    client.PhoneNumber);
                existingClient.SetDateOfBirth(client.DateOfBirth);
                existingClient.SetGender(client.Gender);
                await _clientRepository.UpdateAsync(existingClient);
                return ServiceResult<ClientDto>.Success(_mapper.Map<ClientDto>(existingClient));
            }
            catch (Exception ex)
            {
                return ServiceResult<ClientDto>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> DeleteClientAsync(int id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                if (client == null)
                    return ServiceResult<bool>.Failure("Клиент не найден");
                await _clientRepository.DeleteAsync(id);
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex) 
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> EmailExistsAsync(string email)
        {
            try
            {
                var exists = await _clientRepository.EmailExistsAsync(email);
                return ServiceResult<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
