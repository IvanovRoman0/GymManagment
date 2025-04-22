using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;

namespace GymManagment.Infrastructure.Repositories
{
    public interface IPaymentClientMembershipRepository
    {
        Task AddAsync(PaymentClientMembership entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int paymentId, int clientMembershipId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int paymentId, int clientMembershipId, CancellationToken cancellationToken = default);
    }
}
