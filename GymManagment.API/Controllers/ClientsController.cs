using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Services.Interfaces;
using GymManagement.Core.Results;
using Microsoft.AspNetCore.Razor.TagHelpers;

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
        public async Task<IActionResult> Create([FromBody] ClientDto clientDto)
        {
            var result = await _clientService.CreateClientAsync(clientDto);
            return result.ToActionResult();
        }

        [HttpGet("выбор id клиента")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _clientService.GetClientByIdAsync(id);
            return result.ToActionResult();
        }
        [HttpPut("изменения клиента")]
        public async Task<IActionResult>Update(int id, [FromBody] ClientDto clientDto)
        {
            var result = await _clientService.UpdateClientAsync(id, clientDto);
            return result.ToActionResult();
        }
        [HttpDelete("Удаление клиента")]
        public async Task<IActionResult>Delete(int id)
        {
            var result = await _clientService.DeleteClientAsync(id);
            return result.ToActionResult();
        }
    }
}