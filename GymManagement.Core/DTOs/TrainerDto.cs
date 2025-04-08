using System;
using GymManagement.Core.Entities;


namespace GymManagement.Core.DTOs
{
    public class TrainerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? SpecializationId { get; set; }
        public string SpecializationName { get; set; }
    }
}
