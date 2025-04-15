using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.DTOs
{
    public class ClassDto
    {
        public int Id { get; set; }
        public int? TrainerId { get; set; }
        public string TrainerName { get; set; }
        public int GymId { get; set; }
        public string GymName { get; set; }
        public string ClassName { get; set; }
        public string ClassType { get; set; } 
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
