using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;

namespace GymManagement.Services.Mapping
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentDto, Payment>()
                .ConstructUsing(paymentDto => Payment.Create(
                    paymentDto.ClientId,
                    paymentDto.PaymentDate,
                    paymentDto.Amount,
                    paymentDto.PaymentType))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Payment, PaymentDto>();
        }
    }
}
