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
    public class EquipmentProfile : Profile
    {
        public EquipmentProfile()
        {
            CreateMap<EquipmentDto, Equipment>()
                .ConstructUsing(equipmentDto => Equipment.Create(equipmentDto.EquipmentName, equipmentDto.GymId, equipmentDto.DatePurchase))
                .ForMember(dest => dest.id, opt => opt.Ignore());

            CreateMap<Equipment, EquipmentDto>();
           
        }
    }
}
