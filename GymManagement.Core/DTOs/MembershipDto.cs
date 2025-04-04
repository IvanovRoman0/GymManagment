using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.DTOs
{
    public class MembershipDto
    {
        public int Id { get; set; }
        public string MembershipType { get; set; }
        public decimal Price { get; set; } 

    }
}
