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
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost("Создание оборудования")]
        public async Task<IActionResult> Create([FromBody] EquipmentDto equipmentDto, CancellationToken cancellationToken)
        {
            var result = await _equipmentService.CreateEquipmentAsync(equipmentDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("все оборудование")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _equipmentService.GetAllEquipmentAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("Изменения оборудования")]
        public async Task<IActionResult> Update(int id, [FromBody] EquipmentDto equipmentDto, CancellationToken cancellationToken)
        {
            var result = await _equipmentService.UpdateEquipmentAsync(id, equipmentDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("Удаление оборудования")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _equipmentService.DeleteEquipmentAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
