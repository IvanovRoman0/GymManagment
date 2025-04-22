using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("Созание отзыва")]
        public async Task<IActionResult> Create([FromBody] ReviewDto dto, CancellationToken cancellationToken)
            => (await _reviewService.CreateReviewAsync(dto, cancellationToken)).ToActionResult();

        [HttpGet("Все отзывы")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            => (await _reviewService.GetAllReviewsAsync(cancellationToken)).ToActionResult();

        [HttpPut("Изменения отзывов")]
        public async Task<IActionResult> Update(int id, [FromBody] ReviewDto dto, CancellationToken cancellationToken)
            => (await _reviewService.UpdateReviewAsync(id, dto, cancellationToken)).ToActionResult();

        [HttpDelete("Удаление отзыва")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
            => (await _reviewService.DeleteReviewAsync(id, cancellationToken)).ToActionResult();
    }
}
