using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using GymManagement.Core.Results;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/trainers")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        [HttpPost("создание тренера")]
        public async Task<IActionResult> Create([FromBody] TrainerDto trainerDto, CancellationToken cancellationToken)
        {
            var result = await _trainerService.CreateTrainerAsync(trainerDto, cancellationToken);
            return result.ToActionResult();
        }
        [HttpGet("Все тренера")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _trainerService.GetAllTrainerAsync(cancellationToken);
            return result.ToActionResult();
        }
        [HttpPut("изменения тренера")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainerDto trainerDto, CancellationToken cancellationToken)
        {
            var result = await _trainerService.UpdateTrainerAsync(id, trainerDto, cancellationToken);
            return result.ToActionResult();
        }
        [HttpDelete("удаление тренера")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _trainerService.DeleteTrainerAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
