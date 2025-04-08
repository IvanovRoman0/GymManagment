
namespace GymManagement.Core.Entities
{
    public class Membership : BaseEntity
    {
        protected Membership() { }
        public Membership(string membershipType, decimal price) 
        {
            MembershipType = membershipType;
            Price = price;
        }
        public string MembershipType { get; set; }
        public decimal Price { get; set; }

        public static Membership Create(string membershipType, decimal price)
        {
            return new Membership(membershipType, price);
        }
    }
}
