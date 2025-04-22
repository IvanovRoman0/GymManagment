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
    public class PaymentClientMembershipProfile : Profile
    {
        public PaymentClientMembershipProfile()
        {
            CreateMap<PaymentClientMembership, PaymentClientMembershipDto>();
            CreateMap<PaymentClientMembershipDto, PaymentClientMembership>();
        }
    }
}
