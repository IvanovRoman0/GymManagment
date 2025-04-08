using System;
using GymManagement.Core.Entities;

namespace GymManagement.Core.DTOs
{
    public class MembershipDto
    {
        public int Id { get; set; }
        public string MembershipType { get; set; }
        public decimal Price { get; set; } 

    }
}
