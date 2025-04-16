using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Payment : BaseEntity
    {
        protected Payment() { }

        public Payment(int clientId, DateTime paymentDate, decimal amount, string paymentType)
        {
            ClientId = clientId;
            PaymentDate = paymentDate;
            Amount = amount;
            PaymentType = paymentType;
        }

        public int ClientId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }

        public virtual Client Client { get; set; }

        public static Payment Create(int clientId, DateTime paymentDate, decimal amount, string paymentType)
        {
            return new Payment(clientId, paymentDate, amount, paymentType);
        }
    }
}
