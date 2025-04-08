using System;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public DateTime RegistrationDate { get; set; }

        public static Client Create(
            string firstName,
            string lastName,
            string phoneNumber, 
            string email,
            DateTime? dateOfBirth,
            Gender? gender)
        {
            var client = new Client(firstName, lastName, phoneNumber, email)
            {
                DateOfBirth = dateOfBirth,
                Gender = gender
            }; 
            return client;
        }
    }
}
