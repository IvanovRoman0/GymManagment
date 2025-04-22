using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Workout : BaseEntity
    {
        protected Workout() { }
        public Workout (int clientId, string workoutType, int duration, DateTime datetime, int gymId)
        {
            ClientId = clientId;
            WorkoutType = workoutType;
            Duration = duration;
            DateTime = datetime;
            GymId = gymId;
        }
        public int ClientId { get; set; }
        public string WorkoutType { get; set; }
        public int Duration { get; set; }
        public DateTime DateTime { get; set; }
        public int GymId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Gym Gym { get; set; }
        public static Workout Create(int clientId, string workoutType, int duration, DateTime datetime, int gymId)
        {
            return new Workout(clientId, workoutType, duration, datetime, gymId);
        }
    }
}
