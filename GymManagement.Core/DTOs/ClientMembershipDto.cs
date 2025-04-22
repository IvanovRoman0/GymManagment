using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.DTOs
{
    public class ClientMembershipDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int MembershipId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
