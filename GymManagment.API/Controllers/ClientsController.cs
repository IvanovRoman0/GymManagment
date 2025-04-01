using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Services.Interfaces;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var client = new Client(
                    clientDto.FirstName,
                    clientDto.LastName,
                    clientDto.PhoneNumber,
                    clientDto.Email);

                if (clientDto.DateOfBirth.HasValue)
                {
                    client.SetDateOfBirth(clientDto.DateOfBirth.Value);
                }

                if (!string.IsNullOrEmpty(clientDto.Gender))
                {
                    client.SetGender(clientDto.Gender);
                }

                await _clientService.AddAsync(client);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = client.Id },
                    MapToDto(client));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("выбор id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var client = await _clientService.GetByIdAsync(id);
                if (client == null)
                {
                    return NotFound();
                }

                return Ok(MapToDto(client));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        private ClientDto MapToDto(Client client)
        {
            return new ClientDto
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                DateOfBirth = client.DateOfBirth,
                Gender = client.Gender,
                RegistrationDate = client.RegistrationDate
            };
        }
    }
}