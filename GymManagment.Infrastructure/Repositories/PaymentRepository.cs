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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly GymDbContext _context;

        public PaymentRepository(GymDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Include(p => p.Client)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Include(p => p.Client)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Payment>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Where(p => p.ClientId == clientId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Include(p => p.Client)
                .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Payment payment, CancellationToken cancellationToken)
        {
            await _context.Payments.AddAsync(payment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Payment payment, CancellationToken cancellationToken)
        {
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .AnyAsync(p => p.Id == id, cancellationToken);
        }
        //public async Task<bool> HasLinkedMembershipsAsync(int paymentId, CancellationToken cancellationToken)
        //{
        //    return await _context.PaymentClientMemberships
        //        .AnyAsync(pcm => pcm.PaymentId == paymentId, cancellationToken);
        //}
    }
}
