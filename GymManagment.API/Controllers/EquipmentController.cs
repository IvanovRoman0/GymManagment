using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/equipment")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _service;

        public EquipmentController(IEquipmentService service)
        {
            _service = service;
        }

        [HttpPost("Создание оборудования")]
        public async Task<IActionResult> Create([FromBody] EquipmentDto equipmentDto, CancellationToken cancellationToken)
        {
            var result = await _service.CreateEquipmentAsync(equipmentDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("Выбор оборудования")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetEquipmentByIdAsync(id, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllEquipmentAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("gym/{gymId}")]
        public async Task<IActionResult> GetByGymId(int gymId, CancellationToken cancellationToken)
        {
            var result = await _service.GetEquipmentByGymIdAsync(gymId, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("Изменения оборудования")]
        public async Task<IActionResult> Update(int id, [FromBody] EquipmentDto equipmentDto, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateEquipmentAsync(id, equipmentDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("Удаление оборудования")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteEquipmentAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
