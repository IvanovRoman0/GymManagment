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
    public class SpecializationProfile : Profile
    {
        public SpecializationProfile()
        {
            CreateMap<SpecializationDto, Specialization>()
                .ConstructUsing(specializationDto => Specialization.Create(specializationDto.SpecializationName))
                .ForMember(dest => dest.id, opt => opt.Ignore());
            CreateMap<Specialization, SpecializationDto>();
        }
    }
}
