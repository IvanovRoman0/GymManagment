using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Core.Results;
using GymManagement.Services.Interfaces;
using GymManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GymManagment.Infrastructure.Repositories;
using AutoMapper;

namespace GymManagement.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IGymRepository _gymRepository;

        public ReviewService(
            IReviewRepository reviewRepository,
            IMapper mapper,
            IClientRepository clientRepository,
            ITrainerRepository trainerRepository,
            IGymRepository gymRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _trainerRepository = trainerRepository;
            _gymRepository = gymRepository;
        }

        public async Task<ServiceResult<ReviewDto>> CreateReviewAsync(ReviewDto reviewDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _clientRepository.ExistsAsync(reviewDto.ClientId, cancellationToken))
                    return ServiceResult<ReviewDto>.Failure("Клиент не найден", 404);

                if (reviewDto.TrainerId.HasValue &&
                    !await _trainerRepository.ExistsAsync(reviewDto.TrainerId.Value, cancellationToken))
                    return ServiceResult<ReviewDto>.Failure("Тренер не найден", 404);

                if (reviewDto.GymId.HasValue &&
                    !await _gymRepository.ExistsAsync(reviewDto.GymId.Value, cancellationToken))
                    return ServiceResult<ReviewDto>.Failure("Зал не найден", 404);

                var review = _mapper.Map<Reviews>(reviewDto);
                await _reviewRepository.AddAsync(review, cancellationToken);
                reviewDto.Id = review.id;
                return ServiceResult<ReviewDto>.Success(reviewDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<ReviewDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<ReviewDto>> GetReviewByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(id, cancellationToken);
                if (review == null)
                    return ServiceResult<ReviewDto>.Failure("Отзыв не найден", 404);

                return ServiceResult<ReviewDto>.Success(_mapper.Map<ReviewDto>(review));
            }
            catch (Exception ex)
            {
                return ServiceResult<ReviewDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ReviewDto>>> GetAllReviewsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var reviews = await _reviewRepository.GetAllAsync(cancellationToken);
                return ServiceResult<IEnumerable<ReviewDto>>.Success(_mapper.Map<IEnumerable<ReviewDto>>(reviews));
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ReviewDto>>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<ReviewDto>> UpdateReviewAsync(int id, ReviewDto reviewDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id != reviewDto.Id)
                    return ServiceResult<ReviewDto>.Failure("ID отзыва не совпадает");

                var existingReview = await _reviewRepository.GetByIdAsync(id, cancellationToken);
                if (existingReview == null)
                    return ServiceResult<ReviewDto>.Failure("Отзыв не найден", 404);


                if (!await _clientRepository.ExistsAsync(reviewDto.ClientId, cancellationToken))
                    return ServiceResult<ReviewDto>.Failure("Клиент не найден", 404);

                if (reviewDto.TrainerId.HasValue &&
                    !await _trainerRepository.ExistsAsync(reviewDto.TrainerId.Value, cancellationToken))
                    return ServiceResult<ReviewDto>.Failure("Тренер не найден", 404);

                if (reviewDto.GymId.HasValue &&
                    !await _gymRepository.ExistsAsync(reviewDto.GymId.Value, cancellationToken))
                    return ServiceResult<ReviewDto>.Failure("Зал не найден", 404);

                _mapper.Map(reviewDto, existingReview);
                await _reviewRepository.UpdateAsync(existingReview, cancellationToken);
                return ServiceResult<ReviewDto>.Success(_mapper.Map<ReviewDto>(existingReview));
            }
            catch (Exception ex)
            {
                return ServiceResult<ReviewDto>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> DeleteReviewAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(id, cancellationToken);
                if (review == null)
                    return ServiceResult<bool>.Failure("Отзыв не найден", 404);

                await _reviewRepository.DeleteAsync(id, cancellationToken);
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }
    }
}
