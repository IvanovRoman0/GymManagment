using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GymManagment.Infrastructure.Repositories
{
    public class PaymentClientMembershipRepository : IPaymentClientMembershipRepository
    {
        private readonly GymDbContext _context;

        public PaymentClientMembershipRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PaymentClientMembership entity, CancellationToken cancellationToken)
        {
            await _context.PaymentClientMemberships.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int paymentId, int clientMembershipId, CancellationToken cancellationToken)
        {
            var entity = await _context.PaymentClientMemberships
                .FirstOrDefaultAsync(pcm => pcm.PaymentId == paymentId && pcm.ClientMembershipId == clientMembershipId, cancellationToken);

            if (entity != null)
            {
                _context.PaymentClientMemberships.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> ExistsAsync(int paymentId, int clientMembershipId, CancellationToken cancellationToken)
        {
            return await _context.PaymentClientMemberships
                .AnyAsync(pcm => pcm.PaymentId == paymentId && pcm.ClientMembershipId == clientMembershipId, cancellationToken);
        }
    }
}
