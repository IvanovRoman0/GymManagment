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
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<WorkoutDto, Workout>()
                .ConstructUsing(workoutDto => Workout.Create(
                    workoutDto.ClientId,
                    workoutDto.WorkoutType,
                    workoutDto.Duration,
                    workoutDto.DateTime,
                    workoutDto.GymId))
                .ForMember(dest => dest.id, opt => opt.Ignore());

            CreateMap<Workout, WorkoutDto>();
        }
    }
}