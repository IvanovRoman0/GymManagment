using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Services.Interfaces;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.Repositories;

namespace GymManagement.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Client> GetByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _clientRepository.GetAllAsync();
        }
        public async Task AddAsync(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            await _clientRepository.AddAsync(client);
        }
        public async Task UpdateAsync(int id, Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            var existingClient = await _clientRepository.GetByIdAsync(id);
            if (existingClient == null) throw new KeyNotFoundException("клиент не найден");

            existingClient.UpdatePersonalInfo(client.FirstName, client.LastName, client.PhoneNumber);
            existingClient.SetDateOfBirth(client.DateOfBirth);
            existingClient.SetGender(client.Gender);
            await _clientRepository.UpdateAsync(existingClient);
        }
        public async Task DeleteAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }
    }
}
