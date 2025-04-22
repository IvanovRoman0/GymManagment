using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.DTOs;
using GymManagement.Core.Results;

namespace GymManagement.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ServiceResult<ReviewDto>> CreateReviewAsync(ReviewDto reviewDto, CancellationToken cancellationToken);
        Task<ServiceResult<ReviewDto>> GetReviewByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<ReviewDto>>> GetAllReviewsAsync(CancellationToken cancellationToken);
        Task<ServiceResult<ReviewDto>> UpdateReviewAsync(int id, ReviewDto reviewDto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeleteReviewAsync(int id, CancellationToken cancellationToken);
    }
}
