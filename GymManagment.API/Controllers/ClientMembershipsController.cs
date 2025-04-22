using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/client-memberships")]
    public class ClientMembershipsController : ControllerBase
    {
        private readonly IClientMembershipService _service;

        public ClientMembershipsController(IClientMembershipService service)
        {
            _service = service;
        }

        [HttpPost("Создание абонемента клиента")]
        public async Task<IActionResult> Create([FromBody] ClientMembershipDto dto, CancellationToken cancellationToken)
            => (await _service.CreateClientMembershipAsync(dto, cancellationToken)).ToActionResult();

        [HttpGet("Получить абонемент клиента")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
            => (await _service.GetClientMembershipByIdAsync(id, cancellationToken)).ToActionResult();

        [HttpGet("Все абонементы клиентов")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            => (await _service.GetAllClientMembershipsAsync(cancellationToken)).ToActionResult();

        [HttpPut("Изменение абонемента клиента")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientMembershipDto dto, CancellationToken cancellationToken)
            => (await _service.UpdateClientMembershipAsync(id, dto, cancellationToken)).ToActionResult();

        [HttpDelete("Удаление абонемента клиента")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
            => (await _service.DeleteClientMembershipAsync(id, cancellationToken)).ToActionResult();
    }
}
