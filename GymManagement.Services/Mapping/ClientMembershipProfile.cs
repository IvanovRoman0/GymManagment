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
    public class ClientMembershipProfile : Profile
    {
        public ClientMembershipProfile()
        {
            CreateMap<ClientMembershipDto, ClientMembership>()
                .ConstructUsing(clientmembershipDto => ClientMembership.Create(
                   clientmembershipDto.ClientId,
                   clientmembershipDto.MembershipId,
                   clientmembershipDto.DateStart,
                   clientmembershipDto.DateEnd))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ClientMembership, ClientMembershipDto>();
        }
    }
}
