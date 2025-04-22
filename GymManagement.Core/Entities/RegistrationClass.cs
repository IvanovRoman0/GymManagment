using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class RegistrationClass : BaseEntity
    {
        protected RegistrationClass() { }

        public RegistrationClass(int clientId, DateTime registrationDate, int classId)
        {
            ClientId = clientId;
            RegistrationDate = registrationDate;
            ClassId = classId;
        }

        public int ClientId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int ClassId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Class Class { get; set; }

        public static RegistrationClass Create(int clientId, DateTime registrationDate, int classId)
        {
            return new RegistrationClass(clientId, registrationDate, classId);
        }
    }
}
