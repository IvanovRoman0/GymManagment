using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymManagement.Core.Entities;
using GymManagement.Core.DTOs;
namespace GymManagement.Services.Mapping
{
    public class ClientProfile : Profile
    {
        public ClientProfile() 
        {
            CreateMap<ClientDto, Client>()
                .ConstructUsing(clientdto => Client.Create(
                    clientdto.FirstName,
                    clientdto.LastName,
                    clientdto.PhoneNumber,
                    clientdto.Email,
                    clientdto.DateOfBirth,
                    clientdto.Gender))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore());
            CreateMap<Client, ClientDto>();

        }
    }
}
