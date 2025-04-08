using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("Создание клиента")]
        public async Task<IActionResult> Create([FromBody] ClientDto clientDto, CancellationToken cancellationToken)
        {
            var result = await _clientService.CreateClientAsync(clientDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("выбор id клиента")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _clientService.GetClientByIdAsync(id, cancellationToken);
            return result.ToActionResult();
        }
        [HttpPut("изменения клиента")]
        public async Task<IActionResult>Update(int id, [FromBody] ClientDto clientDto, CancellationToken cancellationToken)
        {
            var result = await _clientService.UpdateClientAsync(id, clientDto, cancellationToken);
            return result.ToActionResult();
        }
        [HttpDelete("Удаление клиента")]
        public async Task<IActionResult>Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _clientService.DeleteClientAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}