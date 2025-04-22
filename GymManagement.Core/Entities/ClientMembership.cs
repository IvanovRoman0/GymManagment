using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class ClientMembership : BaseEntity
    {
        protected ClientMembership() { }

        public ClientMembership(int clientId, int membershipId, DateTime dateStart, DateTime dateEnd)
        {
            ClientId = clientId;
            MembershipId = membershipId;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public int ClientId { get; set; }
        public int MembershipId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public virtual Client Client { get; set; }
        public virtual Membership Membership { get; set; }

        public static ClientMembership Create(int clientId, int membershipId, DateTime dateStart, DateTime dateEnd)
        {
            return new ClientMembership(clientId, membershipId, dateStart, dateEnd);
        }
    }
}
