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
    public class RegistrationClassProfile : Profile
    {
        public RegistrationClassProfile()
        {
            CreateMap<RegistrationClassDto, RegistrationClass>()
                .ConstructUsing(registrationclassDto => RegistrationClass.Create(
                    registrationclassDto.ClientId,
                    registrationclassDto.RegistrationDate,
                    registrationclassDto.ClassId))
                .ForMember(dest => dest.id, opt => opt.Ignore());

            CreateMap<RegistrationClass, RegistrationClassDto>();
        }
    }
}
