using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Reviews : BaseEntity
    {
        protected Reviews() { }
        public int ClientId { get; set; }
        public int? TrainerId { get; set; }
        public int? GymId { get; set; }
        public int Raiting { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        public virtual Client Client { get; set; }
        public virtual Trainer Trainer { get; set; }
        public virtual Gym Gym { get; set; }
    }
}
