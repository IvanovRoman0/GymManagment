using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;

namespace GymManagment.API.Controllers
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
        public async Task<ActionResult<MembershipDto>> Create([FromBody] MembershipDto membershipDto)
        {
            await _membershipService.AddAsync(membershipDto);
            var createdMembership = await _membershipService.GetByIdAsync(membershipDto.Id);
            return CreatedAtAction(nameof(GetById), new {id = membershipDto.Id}, membershipDto);
        }

        [HttpGet("выбор id абонимента")]
        public async Task<ActionResult<MembershipDto>> GetById(int id)
        {
            var result = await _membershipService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
        [HttpPut("изменения абонимента")]
        public async Task<IActionResult> Update(int id, MembershipDto membershipDto)
        {
            if (id != membershipDto.Id) return BadRequest();
            await _membershipService.UpdateAsync(id, membershipDto);
            return NoContent();
        }
        [HttpDelete("удаление абонимента")]
        public async Task<IActionResult> Delete(int id)
        {
            await _membershipService.DeleteAsync(id);
            return NoContent();
        }

    }
}
