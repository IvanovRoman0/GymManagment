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
    public interface IPaymentClientMembershipService
    {
        Task<ServiceResult> LinkPaymentToMembershipAsync(PaymentClientMembershipDto paymentclientmembershipDto, CancellationToken cancellationToken);
        Task<ServiceResult> UnlinkPaymentFromMembershipAsync(int paymentId, int clientMembershipId, CancellationToken cancellationToken);
    }
}
