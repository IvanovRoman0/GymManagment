using AutoMapper;
using GymManagement.Core.Entities;
using GymManagement.Core.DTOs;

namespace GymManagement.Services.Mapping
{
    public class MembershipProfile : Profile
    {
        public MembershipProfile() 
        {
            CreateMap<MembershipDto, Membership>()
                .ConstructUsing(membershipDto => new Membership(membershipDto.MembershipType, membershipDto.Price))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Membership, MembershipDto>();
        }
    }
}
