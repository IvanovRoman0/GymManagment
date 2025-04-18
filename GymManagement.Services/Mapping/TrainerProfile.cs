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
    public class TrainerProfile : Profile
    {
        public TrainerProfile() 
        {
            CreateMap<TrainerDto, Trainer>()
                .ConstructUsing(trainerDto => Trainer.Create(
                    trainerDto.FirstName,
                    trainerDto.LastName,
                    trainerDto.PhoneNumber,
                    trainerDto.Email,
                    trainerDto.SpecializationId))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Trainer, TrainerDto>();
        }
    }
}
