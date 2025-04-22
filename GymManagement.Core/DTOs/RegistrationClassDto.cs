using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.DTOs
{
    public class RegistrationClassDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int ClassId { get; set; }
    }
}
