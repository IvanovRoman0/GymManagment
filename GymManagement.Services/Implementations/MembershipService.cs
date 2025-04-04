using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Services.Interfaces;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagment.Infrastructure.Repositories;
using System.Runtime.InteropServices;
using AutoMapper;

namespace GymManagement.Services.Implementations
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _MembershipRepository;
        private readonly IMapper _mapper;
        public MembershipService(IMembershipRepository membershipRepository, IMapper mapper)
        {
            _MembershipRepository = membershipRepository;
            _mapper = mapper;
        }
        public async Task<MembershipDto> GetByIdAsync(int id)
        {
            var membership = await _MembershipRepository.GetByIdAsync(id);
            return _mapper.Map<MembershipDto>(membership);
        }
        public async Task<IEnumerable<MembershipDto>> GetAllAsync()
        {
            var membership = await _MembershipRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MembershipDto>>(membership);
        }
        public async Task AddAsync(MembershipDto membershipDto)
        {
            var membership = new Membership(membershipDto.MembershipType, membershipDto.Price);
            await _MembershipRepository.AddAsync(membership);
            membershipDto.Id = membership.Id;
            
        }
        public async Task UpdateAsync(int id, MembershipDto membershipDto)
        {
            var membership = await _MembershipRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Абонемент не найден");
            _mapper.Map(membershipDto, membership);
            await _MembershipRepository.UpdateAsync(membership);
        }
        public async Task DeleteAsync(int id)
        {
            await _MembershipRepository.DeleteAsync(id);
        }
    }
}
