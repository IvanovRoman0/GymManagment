using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Class : BaseEntity
    {
        protected Class() { }

        public Class(int gymId, string className, string classType, int? trainerId = null)
        {
            GymId = gymId;
            ClassName = className;
            ClassType = classType;
            TrainerId = trainerId;
            DateTime = DateTime.Now;
        }

        public int? TrainerId { get; set; }
        public int GymId { get; set; }
        public string ClassName { get; set; }
        public string ClassType { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Trainer Trainer { get; set; }
        public virtual Gym Gym { get; set; }

        public static Class Create(int gymId, string className, string classType, int? trainerId = null)
        {
            return new Class(gymId, className, classType, trainerId);
        }
    }
}
