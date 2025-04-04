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
        public async Task<ClientDto> GetByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            return client == null ? null : _mapper.Map<ClientDto>(client);
        }
        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var client = await _clientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(client);
        }
        public async Task AddAsync(ClientDto clientDto)
        {
            var client = Client.Create(clientDto.FirstName, clientDto.LastName, clientDto.PhoneNumber, clientDto.Email, clientDto.DateOfBirth, clientDto.Gender);
            await _clientRepository.AddAsync(client);
            clientDto.Id = client.Id;
        }
        public async Task UpdateAsync(int id, ClientDto clientDto)
        {
            var client = await _clientRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Клиент не найден");
            _mapper.Map(clientDto, client);
            await _clientRepository.UpdateAsync(client);
        }
        public async Task DeleteAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }
    }
}
