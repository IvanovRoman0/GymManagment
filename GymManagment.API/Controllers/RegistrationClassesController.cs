using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/registration-classes")]
    public class RegistrationClassesController : ControllerBase
    {
        private readonly IRegistrationClassService _service;

        public RegistrationClassesController(IRegistrationClassService service)
        {
            _service = service;
        }

        [HttpPost("Создание регистрации")]
        public async Task<IActionResult> Create([FromBody] RegistrationClassDto dto, CancellationToken cancellationToken)
            => (await _service.CreateRegistrationAsync(dto, cancellationToken)).ToActionResult();

        [HttpGet("Получить регистрацию")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
            => (await _service.GetRegistrationByIdAsync(id, cancellationToken)).ToActionResult();

        [HttpGet("Все регистрации")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            => (await _service.GetAllRegistrationsAsync(cancellationToken)).ToActionResult();

        [HttpPut("Изменение регистрации")]
        public async Task<IActionResult> Update(int id, [FromBody] RegistrationClassDto dto, CancellationToken cancellationToken)
            => (await _service.UpdateRegistrationAsync(id, dto, cancellationToken)).ToActionResult();

        [HttpDelete("Удаление регистрации")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
            => (await _service.DeleteRegistrationAsync(id, cancellationToken)).ToActionResult();
    }
}
