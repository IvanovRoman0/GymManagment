using System;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Services.Interfaces;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.Repositories;
using AutoMapper;
using GymManagement.Core.Results;
using System.Collections.Generic;

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
        public async Task<ServiceResult<MembershipDto>> CreateMembershipAsync(MembershipDto membershipDto, CancellationToken cancellationToken)
        {
                if (await _MembershipRepository.ExistsByTypeAsync(membershipDto.MembershipType, cancellationToken))
                    return ServiceResult<MembershipDto>.Failure("Тип абонемента уже существует");
                var membership = Membership.Create(membershipDto.MembershipType, membershipDto.Price);
                await _MembershipRepository.AddAsync(membership, cancellationToken);
                membershipDto.Id = membership.id;
                return ServiceResult<MembershipDto>.Success(membershipDto);
        }
        public async Task<ServiceResult<IEnumerable<MembershipDto>>> GetAllMembershipAsync(CancellationToken cancellationToken)
        {
                var membership = await _MembershipRepository.GetAllAsync(cancellationToken);
                var membershipDtos = _mapper.Map<IEnumerable<MembershipDto>>(membership);
                return ServiceResult<IEnumerable<MembershipDto>>.Success(membershipDtos);
        }
        public async Task<ServiceResult<MembershipDto>> GetMembershipByIdAsync(int id, CancellationToken cancellationToken)
        {
            var membership = await _MembershipRepository.GetByIdAsync(id, cancellationToken);
            return membership == null ? ServiceResult<MembershipDto>.Failure("Абонемент не найден", 404)
                : ServiceResult<MembershipDto>.Success(_mapper.Map<MembershipDto>(membership));
        }
        public async Task<ServiceResult<MembershipDto>> UpdateMembershipAsync(int id, MembershipDto membershipDto, CancellationToken cancellationToken)
        {
                if (id != membershipDto.Id)
                    return ServiceResult<MembershipDto>.Failure("ID абонемента не совпадает");
                var existingMembership = await _MembershipRepository.GetByIdAsync(id, cancellationToken);
                if (existingMembership == null)
                    return ServiceResult<MembershipDto>.Failure("абонемент не найден", 404);
                existingMembership.MembershipType = membershipDto.MembershipType;
                existingMembership.Price = membershipDto.Price;
                await _MembershipRepository.UpdateAsync(existingMembership, cancellationToken);
                return ServiceResult<MembershipDto>.Success(_mapper.Map<MembershipDto>(existingMembership));
        }
        public async Task<ServiceResult<bool>> DeleteMembershipAsync(int id, CancellationToken cancellationToken)
        {
                var membership = await _MembershipRepository.GetByIdAsync(id, cancellationToken);
                if (membership == null)
                    return ServiceResult<bool>.Failure("Абонемент не найден", 404);
                await _MembershipRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
        }
        public async Task<ServiceResult<bool>> MembershipTypeExistsAsync(string membershipType, CancellationToken cancellationToken)
        {
                var exists = await _MembershipRepository.ExistsByTypeAsync(membershipType, cancellationToken);
                return ServiceResult<bool>.Success(exists);

        }
    }
}
