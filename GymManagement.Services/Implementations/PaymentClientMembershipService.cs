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
    public class PaymentClientMembershipService : IPaymentClientMembershipService
    {
        private readonly IPaymentClientMembershipRepository _repository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IClientMembershipRepository _clientMembershipRepository;
        private readonly IMapper _mapper;

        public PaymentClientMembershipService(
            IPaymentClientMembershipRepository repository,
            IPaymentRepository paymentRepository,
            IClientMembershipRepository clientMembershipRepository,
            IMapper mapper)
        {
            _repository = repository;
            _paymentRepository = paymentRepository;
            _clientMembershipRepository = clientMembershipRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult> LinkPaymentToMembershipAsync(PaymentClientMembershipDto paymentclientmembershipDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _paymentRepository.ExistsAsync(paymentclientmembershipDto.PaymentId, cancellationToken))
                    return ServiceResult.Failure("Платеж не найден", 404);

                if (!await _clientMembershipRepository.ExistsAsync(paymentclientmembershipDto.ClientMembershipId, cancellationToken))
                    return ServiceResult.Failure("Абонемент клиента не найден", 404);

                if (await _repository.ExistsAsync(paymentclientmembershipDto.PaymentId, paymentclientmembershipDto.ClientMembershipId, cancellationToken))
                    return ServiceResult.Failure("Связь уже существует");

                var entity = new PaymentClientMembership
                {
                    PaymentId = paymentclientmembershipDto.PaymentId,
                    ClientMembershipId = paymentclientmembershipDto.ClientMembershipId
                };

                await _repository.AddAsync(entity, cancellationToken);
                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult> UnlinkPaymentFromMembershipAsync(int paymentId, int clientMembershipId, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _repository.ExistsAsync(paymentId, clientMembershipId, cancellationToken))
                    return ServiceResult.Failure("Связь не найдена", 404);

                await _repository.DeleteAsync(paymentId, clientMembershipId, cancellationToken);
                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure(ex.Message);
            }
        }
    }
}
