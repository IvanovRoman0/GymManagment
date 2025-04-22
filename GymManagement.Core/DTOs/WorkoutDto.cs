using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.DTOs
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        public string WorkoutType { get; set; }
        public int Duration { get; set; }
        public DateTime DateTime { get; set; }
        public int ClientId { get; set; }
        public int GymId { get; set; }
    }
}
