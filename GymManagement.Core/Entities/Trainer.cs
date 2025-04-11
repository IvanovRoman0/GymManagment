using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Trainer : BaseEntity
    {
        protected Trainer() { }
        public Trainer(
            string firstName,
            string lastName,
            string phoneNumber,
            string email,
            int? specializationId)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            SpecializationId = specializationId;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
        
        public static Trainer Create(
            string firstName,
            string lastName,
            string phoneNumber,
            string email,
            int? specializationId)
        {
            return new Trainer(firstName, lastName, phoneNumber, email, specializationId);
        }
    }
}
