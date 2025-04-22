using GymManagement.Core.DTOs;
using GymManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Core.Results;

namespace GymManagement.API.Controllers
{
    [ApiController]
    [Route("api/payment-client-memberships")]
    public class PaymentClientMembershipsController : ControllerBase
    {
        private readonly IPaymentClientMembershipService _service;

        public PaymentClientMembershipsController(IPaymentClientMembershipService service)
        {
            _service = service;
        }

        [HttpPost("создание")]
        public async Task<IActionResult> Link([FromBody] PaymentClientMembershipDto paymentclientmembershipDto, CancellationToken cancellationToken)
            => (await _service.LinkPaymentToMembershipAsync(paymentclientmembershipDto, cancellationToken)).ToActionResult();

        [HttpDelete("удаление")]
        public async Task<IActionResult> Unlink(int paymentId, int clientMembershipId, CancellationToken cancellationToken)
            => (await _service.UnlinkPaymentFromMembershipAsync(paymentId, clientMembershipId, cancellationToken)).ToActionResult();
    }
}
