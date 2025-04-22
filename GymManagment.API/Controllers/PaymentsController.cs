using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Создание платежа")]
        public async Task<IActionResult> Create([FromBody] PaymentDto paymentDto, CancellationToken cancellationToken)
        {
            var result = await _paymentService.CreatePaymentAsync(paymentDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("Все платежи")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _paymentService.GetAllPaymentsAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut("Изменение платежей")]
        public async Task<IActionResult> Update(int id, [FromBody] PaymentDto paymentDto, CancellationToken cancellationToken)
        {
            var result = await _paymentService.UpdatePaymentAsync(id, paymentDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("Удаление платежей")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _paymentService.DeletePaymentAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
