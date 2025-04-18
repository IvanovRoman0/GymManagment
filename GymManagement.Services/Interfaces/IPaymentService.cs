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
    public interface IPaymentService
    {
        Task<ServiceResult<PaymentDto>> CreatePaymentAsync(PaymentDto dto, CancellationToken cancellationToken);
        Task<ServiceResult<PaymentDto>> GetPaymentByIdAsync(int id, CancellationToken cancellationToken);
        Task<ServiceResult<IEnumerable<PaymentDto>>> GetAllPaymentsAsync(CancellationToken cancellationToken);
        Task<ServiceResult<PaymentDto>> UpdatePaymentAsync(int id, PaymentDto dto, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> DeletePaymentAsync(int id, CancellationToken cancellationToken);
    }
}
