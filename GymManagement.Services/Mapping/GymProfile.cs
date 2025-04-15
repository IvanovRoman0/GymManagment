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
    public class GymProfile : Profile
    {
        public GymProfile()
        {
            CreateMap<GymDto, Gym>()
                .ConstructUsing(gymDto => Gym.Create(gymDto.GymName, gymDto.Location))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Gym, GymDto>();
        }
    }
}
