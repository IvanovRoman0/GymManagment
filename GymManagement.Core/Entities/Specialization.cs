using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Specialization : BaseEntity
    {
        public Specialization() { }
        public Specialization(string specializationName) 
        {
            SpecializationName = specializationName;
        }
        public string SpecializationName { get; set; }

        public static Specialization Create(string specializationName) 
        {
            return new Specialization(specializationName); 
        }
    }
}
