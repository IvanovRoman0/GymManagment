using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class PaymentClientMembership
    {
        public int PaymentId { get; set; }
        public int ClientMembershipId { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual ClientMembership ClientMembership { get; set; }
    }
}
