﻿using System;
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
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IClientRepository clientRepository,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PaymentDto>> CreatePaymentAsync(PaymentDto paymentDto, CancellationToken cancellationToken)
        {
                if (!await _clientRepository.ExistsAsync(paymentDto.ClientId, cancellationToken))
                    return ServiceResult<PaymentDto>.Failure("Клиент не найден", 404);

                var payment = Payment.Create(
                    paymentDto.ClientId,
                    paymentDto.PaymentDate,
                    paymentDto.Amount,
                    paymentDto.PaymentType);

                await _paymentRepository.AddAsync(payment, cancellationToken);
                paymentDto.Id = payment.id;
                return ServiceResult<PaymentDto>.Success(paymentDto);
        }

        public async Task<ServiceResult<PaymentDto>> GetPaymentByIdAsync(int id, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(id, cancellationToken);
            return payment == null
                ? ServiceResult<PaymentDto>.Failure("Платеж не найден", 404)
                : ServiceResult<PaymentDto>.Success(_mapper.Map<PaymentDto>(payment));
        }

        public async Task<ServiceResult<IEnumerable<PaymentDto>>> GetAllPaymentsAsync(CancellationToken cancellationToken)
        {
                var payments = await _paymentRepository.GetAllAsync(cancellationToken);
                return ServiceResult<IEnumerable<PaymentDto>>.Success(_mapper.Map<IEnumerable<PaymentDto>>(payments));
        }

        public async Task<ServiceResult<PaymentDto>> UpdatePaymentAsync(int id, PaymentDto paymentDto, CancellationToken cancellationToken)
        {
                if (id != paymentDto.Id)
                    return ServiceResult<PaymentDto>.Failure("ID платежа не совпадает");

                if (!await _paymentRepository.ExistsAsync(id, cancellationToken))
                    return ServiceResult<PaymentDto>.Failure("Платеж не найден", 404);

                if (!await _clientRepository.ExistsAsync(paymentDto.ClientId, cancellationToken))
                    return ServiceResult<PaymentDto>.Failure("Клиент не найден", 404);

                var payment = Payment.Create(
                    paymentDto.ClientId,
                    paymentDto.PaymentDate,
                    paymentDto.Amount,
                    paymentDto.PaymentType);
                payment.id = id;

                await _paymentRepository.UpdateAsync(payment, cancellationToken);
                return ServiceResult<PaymentDto>.Success(_mapper.Map<PaymentDto>(payment));
        }

        public async Task<ServiceResult<bool>> DeletePaymentAsync(int id, CancellationToken cancellationToken)
        {
                var payment = await _paymentRepository.GetByIdAsync(id, cancellationToken);
                if (payment == null)
                    return ServiceResult<bool>.Failure("Платеж не найден", 404);
                await _paymentRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
        }
    }
}
