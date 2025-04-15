using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/classes")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _service;

        public ClassesController(IClassService service)
        {
            _service = service;
        }

        [HttpPost("Создание занятия")]
        public async Task<IActionResult> Create([FromBody] ClassDto classDto, CancellationToken cancellationToken)
        {
            var result = await _service.CreateClassAsync(classDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("Выбор занятя")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetClassByIdAsync(id, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("Все занятия")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllClassesAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("gym/{gymId}")]
        public async Task<IActionResult> GetByGymId(int gymId, CancellationToken cancellationToken)
        {
            var result = await _service.GetClassesByGymIdAsync(gymId, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("trainer/{trainerId}")]
        public async Task<IActionResult> GetByTrainerId(int trainerId, CancellationToken cancellationToken)
        {
            var result = await _service.GetClassesByTrainerIdAsync(trainerId, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("Изменения занятий")]
        public async Task<IActionResult> Update(int id, [FromBody] ClassDto classDto, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateClassAsync(id, classDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("Удаления занятий")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteClassAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
