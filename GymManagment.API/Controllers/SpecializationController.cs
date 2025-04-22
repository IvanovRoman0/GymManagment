using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using GymManagement.Core.Results;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/specialization")]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;
        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }
        [HttpPost("создание специализации")]
        public async Task<IActionResult> Create([FromBody] SpecializationDto specializationDto, CancellationToken cancellationToken)
        {
            var result = await _specializationService.CreateSpecializationAsync(specializationDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("Все специальзации")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _specializationService.GetAllSpecializationAsync(cancellationToken);
            return result.ToActionResult();
        }
        [HttpPut("изменения специализации")]
        public async Task<IActionResult> Update(int id, [FromBody] SpecializationDto specializationDto, CancellationToken cancellationToken)
        {
            var result = await _specializationService.UpdateSpecializationAsync(id, specializationDto, cancellationToken);
            return result.ToActionResult();
        }
        [HttpDelete("удаление специализации")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _specializationService.DeleteSpecializationAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
