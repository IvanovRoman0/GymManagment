using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/gyms")]
    public class GymsController : ControllerBase
    {
        private readonly IGymService _service;

        public GymsController(IGymService service)
        {
            _service = service;
        }

        [HttpPost("Создание зала")]
        public async Task<IActionResult> Create([FromBody] GymDto dto, CancellationToken cancellationToken)
        {
            var result = await _service.CreateGymAsync(dto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("Выбор зала")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetGymByIdAsync(id, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllGymsAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("Изменение зала")]
        public async Task<IActionResult> Update(int id, [FromBody] GymDto dto, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateGymAsync(id, dto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("Удаление зала")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteGymAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
