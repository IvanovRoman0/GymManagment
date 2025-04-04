using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Membership : BaseEntity
    {
        protected Membership() { }
        public Membership(string membershiptype, decimal price) 
        {
            MembershipType = membershiptype;
            Price = price;
        }
        public string MembershipType { get; private set; }
        public decimal Price { get; private set; }
        
        public void UpdateInfo (string  membershiptype, decimal price)
        {  
            MembershipType = membershiptype;
            Price = price;
        }
        public static Membership Create(string membershiptype, decimal price)
        {
            return new Membership(membershiptype, price);
        }
    }
}
