using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Services.Interfaces;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.Repositories;
using System.Runtime.InteropServices;
using AutoMapper;
using GymManagement.Core.Results;

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
        public async Task<ServiceResult<MembershipDto>> CreateMembershipAsync(MembershipDto membershipDto)
        {
            try
            {
                if (await _MembershipRepository.ExistsByTypeAsync(membershipDto.MembershipType))
                    return ServiceResult<MembershipDto>.Failure("Тип абонемента уже существует");
                var membership = Membership.Create(membershipDto.MembershipType, membershipDto.Price);
                await _MembershipRepository.AddAsync(membership);
                membershipDto.Id = membership.Id;
                return ServiceResult<MembershipDto>.Success(membershipDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<MembershipDto>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<MembershipDto>> GetMembershipByIdAsync(int id)
        {
            var membership = await _MembershipRepository.GetByIdAsync(id);
            return membership == null ? ServiceResult<MembershipDto>.Failure("Абонемент не найден")
                : ServiceResult<MembershipDto>.Success(_mapper.Map<MembershipDto>(membership));
        }
        public async Task<ServiceResult<MembershipDto>> UpdateMembershipAsync(int id, MembershipDto membershipDto)
        {
            try
            {
                if (id != membershipDto.Id)
                    return ServiceResult<MembershipDto>.Failure("ID абонемента не совпадает");
                var existingMembership = await _MembershipRepository.GetByIdAsync(id);
                if (existingMembership == null)
                    return ServiceResult<MembershipDto>.Failure("абонемент не найден");
                existingMembership.UpdateInfo(membershipDto.MembershipType, membershipDto.Price);
                await _MembershipRepository.UpdateAsync(existingMembership);
                return ServiceResult<MembershipDto>.Success(_mapper.Map<MembershipDto>(existingMembership));
            }
            catch (Exception ex)
            {
                return ServiceResult<MembershipDto>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> DeleteMembershipAsync(int id)
        {
            try
            {
                var membership = await _MembershipRepository.GetByIdAsync(id);
                if (membership == null)
                    return ServiceResult<bool>.Failure("Абонемент не найден");
                await _MembershipRepository.DeleteAsync(id);
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
        public async Task<ServiceResult<bool>> MembershipTypeExistsAsync(string membershipType)
        {
            try
            {
                var exists = await _MembershipRepository.ExistsByTypeAsync(membershipType);
                return ServiceResult<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
