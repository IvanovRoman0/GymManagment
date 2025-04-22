using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Core.Results
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; }
        public string Error { get; }
        public int? StatusCode { get; }

        protected ServiceResult(bool isSuccess, T data, string error, int? statusCode = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
            StatusCode = statusCode;
        }

        public static ServiceResult<T> Success(T data) => new ServiceResult<T>(true, data, null);
        public static ServiceResult<T> Failure(string error, int? statusCode = null)
            => new ServiceResult<T>(false, default, error, statusCode);
    }

    public class ServiceResult
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public int? StatusCode { get; }

        protected ServiceResult(bool isSuccess, string error, int? statusCode = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public static ServiceResult Success() => new ServiceResult(true, null);
        public static ServiceResult Failure(string error, int? statusCode = null)
            => new ServiceResult(false, error, statusCode);
    }

    public static class ResultExtensions
    {
        // Для ServiceResult<T> (с данными)
        public static IActionResult ToActionResult<T>(this ServiceResult<T> result)
        {
            if (result.IsSuccess)
            {
                if (result.Data != null)
                {
                    return new OkObjectResult(result.Data);
                }
                return new NoContentResult();
            }

            return CreateErrorResult(result.Error, result.StatusCode);
        }

        // Для ServiceResult (без данных)
        public static IActionResult ToActionResult(this ServiceResult result)
        {
            if (result.IsSuccess)
            {
                return new OkResult(); // Или NoContentResult() по вашему выбору
            }

            return CreateErrorResult(result.Error, result.StatusCode);
        }

        private static IActionResult CreateErrorResult(string error, int? statusCode)
        {
            if (statusCode != null)
            {
                return new ObjectResult(new { Error = error })
                {
                    StatusCode = statusCode
                };
            }
            return new BadRequestObjectResult(new { Error = error });
        }
    }
}