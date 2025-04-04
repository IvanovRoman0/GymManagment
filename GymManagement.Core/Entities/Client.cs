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
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public Gender? Gender { get; private set; }
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

        public void SetGender(Gender? gender)
        {
            Gender = gender;
        }
        public static Client Create(
            string firstName,
            string lastName,
            string phoneNumber, 
            string email,
            DateTime? dateOfBirth,
            Gender? gender)
        {
            var client = new Client(firstName, lastName, phoneNumber, email);
            if (dateOfBirth.HasValue) client.SetDateOfBirth(dateOfBirth.Value); 
            return client;
        }
    }
}
