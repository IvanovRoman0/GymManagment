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
    public class ClientMembershipService : IClientMembershipService
    {
        private readonly IClientMembershipRepository _clientMembershipRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;

        public ClientMembershipService(
            IClientMembershipRepository clientMembershipRepository,
            IClientRepository clientRepository,
            IMembershipRepository membershipRepository,
            IMapper mapper)
        {
            _clientMembershipRepository = clientMembershipRepository;
            _clientRepository = clientRepository;
            _membershipRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ClientMembershipDto>> CreateClientMembershipAsync(ClientMembershipDto clientmembershipDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _clientRepository.ExistsAsync(clientmembershipDto.ClientId, cancellationToken))
                    return ServiceResult<ClientMembershipDto>.Failure("Клиент не найден", 404);

                if (!await _membershipRepository.ExistsAsync(clientmembershipDto.MembershipId, cancellationToken))
                    return ServiceResult<ClientMembershipDto>.Failure("Абонемент не найден", 404);

                if (clientmembershipDto.DateStart >= clientmembershipDto.DateEnd)
                    return ServiceResult<ClientMembershipDto>.Failure("Дата начала должна быть раньше даты окончания");

                var clientMembership = ClientMembership.Create(
                   clientmembershipDto.ClientId,
                   clientmembershipDto.MembershipId,
                   clientmembershipDto.DateStart,
                   clientmembershipDto.DateEnd);

                await _clientMembershipRepository.AddAsync(clientMembership, cancellationToken);
                clientmembershipDto.Id = clientMembership.id;
                return ServiceResult<ClientMembershipDto>.Success(clientmembershipDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<ClientMembershipDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<ClientMembershipDto>> GetClientMembershipByIdAsync(int id, CancellationToken cancellationToken)
        {
            var clientMembership = await _clientMembershipRepository.GetByIdAsync(id, cancellationToken);
            return clientMembership == null
                ? ServiceResult<ClientMembershipDto>.Failure("Абонемент клиента не найден", 404)
                : ServiceResult<ClientMembershipDto>.Success(_mapper.Map<ClientMembershipDto>(clientMembership));
        }

        public async Task<ServiceResult<IEnumerable<ClientMembershipDto>>> GetAllClientMembershipsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var clientMemberships = await _clientMembershipRepository.GetAllAsync(cancellationToken);
                return ServiceResult<IEnumerable<ClientMembershipDto>>.Success(
                    _mapper.Map<IEnumerable<ClientMembershipDto>>(clientMemberships));
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ClientMembershipDto>>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<ClientMembershipDto>> UpdateClientMembershipAsync(int id, ClientMembershipDto clientmembershipDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id != clientmembershipDto.Id)
                    return ServiceResult<ClientMembershipDto>.Failure("ID абонемента клиента не совпадает");

                var existing = await _clientMembershipRepository.GetByIdAsync(id, cancellationToken);
                if (existing == null)
                    return ServiceResult<ClientMembershipDto>.Failure("Абонемент клиента не найден", 404);

                if (!await _clientRepository.ExistsAsync(clientmembershipDto.ClientId, cancellationToken))
                    return ServiceResult<ClientMembershipDto>.Failure("Клиент не найден", 404);

                if (!await _membershipRepository.ExistsAsync(clientmembershipDto.MembershipId, cancellationToken))
                    return ServiceResult<ClientMembershipDto>.Failure("Абонемент не найден", 404);

                if (clientmembershipDto.DateStart >= clientmembershipDto.DateEnd)
                    return ServiceResult<ClientMembershipDto>.Failure("Дата начала должна быть раньше даты окончания");

                existing.ClientId = clientmembershipDto.ClientId;
                existing.MembershipId = clientmembershipDto.MembershipId;
                existing.DateStart = clientmembershipDto.DateStart;
                existing.DateEnd = clientmembershipDto.DateEnd;

                await _clientMembershipRepository.UpdateAsync(existing, cancellationToken);
                return ServiceResult<ClientMembershipDto>.Success(_mapper.Map<ClientMembershipDto>(existing));
            }
            catch (Exception ex)
            {
                return ServiceResult<ClientMembershipDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> DeleteClientMembershipAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var clientMembership = await _clientMembershipRepository.GetByIdAsync(id, cancellationToken);
                if (clientMembership == null)
                    return ServiceResult<bool>.Failure("Абонемент клиента не найден", 404);

                await _clientMembershipRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
