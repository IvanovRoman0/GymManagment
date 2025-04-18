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
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<ClassDto, Class>()
                .ConstructUsing(classDto => Class.Create(
                    classDto.GymId,
                    classDto.ClassName,
                    classDto.ClassType,
                    classDto.TrainerId))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateTime,
                    opt => opt.MapFrom(src => src.DateTime == default ? DateTime.Now : src.DateTime));

            CreateMap<Class, ClassDto>();
        }
    }
}
