using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Gym : BaseEntity
    {
        protected Gym() { }

        public Gym(string gymName, string location)
        {
            GymName = gymName;
            Location = location;
        }

        public string GymName { get; set; }
        public string Location { get; set; }

        public static Gym Create(string gymName, string location)
        {
            return new Gym(gymName, location);
        }
    }
}
