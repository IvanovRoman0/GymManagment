using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/Membership")]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpPost("создание абонимента")]
        public async Task<IActionResult> Create([FromBody] MembershipDto membershipDto, CancellationToken cancellationToken)
        {
           var result = await _membershipService.CreateMembershipAsync(membershipDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("все абонименты")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _membershipService.GetAllMembershipAsync(cancellationToken);
            return result.ToActionResult();
        }
        [HttpPut("изменения абонимента")]
        public async Task<IActionResult> Update(int id, MembershipDto membershipDto, CancellationToken cancellationToken)
        {
            var result = await _membershipService.UpdateMembershipAsync(id, membershipDto, cancellationToken);
            return result.ToActionResult();
        }
        [HttpDelete("удаление абонимента")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _membershipService.DeleteMembershipAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
