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
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentDto paymentDto, CancellationToken cancellationToken)
        {
            var result = await _service.CreatePaymentAsync(paymentDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetPaymentByIdAsync(id, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllPaymentsAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetByClientId(int clientId, CancellationToken cancellationToken)
        {
            var result = await _service.GetPaymentsByClientIdAsync(clientId, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("date-range")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken cancellationToken)
        {
            var result = await _service.GetPaymentsByDateRangeAsync(startDate, endDate, cancellationToken);
            return result.ToActionResult();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PaymentDto paymentDto, CancellationToken cancellationToken)
        {
            var result = await _service.UpdatePaymentAsync(id, paymentDto, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _service.DeletePaymentAsync(id, cancellationToken);
            return result.ToActionResult();
        }
    }
}
