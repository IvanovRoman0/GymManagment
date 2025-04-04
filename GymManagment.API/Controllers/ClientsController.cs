using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Services.Interfaces;
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
            await _clientService.AddAsync(clientDto);
            var createdClient = await _clientService.GetByIdAsync(clientDto.Id);
            return CreatedAtAction(nameof(GetById), new {id = clientDto.Id}, clientDto);
        }

        [HttpGet("выбор id клиента")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            return client == null ? NotFound() : Ok(client);
        }
        [HttpPut("изменения клиента")]
        public async Task<IActionResult>Update(int id, [FromBody] ClientDto clientDto)
        {
            if (id != clientDto.Id) return BadRequest();
            await _clientService.UpdateAsync(id, clientDto);
            return NoContent();
        }
        [HttpDelete("Удаление клиента")]
        public async Task<IActionResult>Delete(int id)
        {
            await _clientService.DeleteAsync(id);
            return NoContent();
        }
    }
}