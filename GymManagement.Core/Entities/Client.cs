using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Client : BaseEntity
    {
        public Client( string firstName, string lastName, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            RegistrationDate = DateTime.Now;
        }
        public int Id { get; protected set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string Gender { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        public void  UpdatePersonalInfo (string firstName, string lastName, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public void SetDateOfBirth(DateTime? dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        public void SetGender(string gender)
        {
            if (gender != "Male" && gender != "Famale")
                throw new ArgumentException("неверный пол");
            Gender = gender;
        }
    }
}
