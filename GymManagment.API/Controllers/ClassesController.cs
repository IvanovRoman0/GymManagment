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
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost("Создание занятия")]
        public async Task<IActionResult> Create([FromBody] ClassDto classDto, CancellationToken cancellationToken)
        {
            var result = await _classService.CreateClassAsync(classDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("Все занятия")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _classService.GetAllClassesAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("Изменения занятий")]
        public async Task<IActionResult> Update(int id, [FromBody] ClassDto classDto, CancellationToken cancellationToken)
        {
            var result = await _classService.UpdateClassAsync(id, classDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("Удаления занятий")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _classService.DeleteClassAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
