using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/workouts")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpPost("Создание тренировки")]
        public async Task<IActionResult> Create([FromBody] WorkoutDto dto, CancellationToken cancellationToken)
            => (await _workoutService.CreateWorkoutAsync(dto, cancellationToken)).ToActionResult();

        [HttpGet("Все тренировки")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            => (await _workoutService.GetAllWorkoutsAsync(cancellationToken)).ToActionResult();

        [HttpPut("Изменение тренировки")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkoutDto dto, CancellationToken cancellationToken)
            => (await _workoutService.UpdateWorkoutAsync(id, dto, cancellationToken)).ToActionResult();

        [HttpDelete("Удаление тренировки")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
            => (await _workoutService.DeleteWorkoutAsync(id, cancellationToken)).ToActionResult();
    }
}