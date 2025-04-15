using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.Entities
{
    public class Equipment : BaseEntity
    {
        protected Equipment() { }

        public Equipment(string equipmentName, int gymId, DateTime? datePurchase = null)
        {
            EquipmentName = equipmentName;
            GymId = gymId;
            DatePurchase = datePurchase;
        }

        public string EquipmentName { get; set; }
        public int GymId { get; set; }
        public DateTime? DatePurchase { get; set; }
        public virtual Gym Gym { get; set; }

        public static Equipment Create(string equipmentName, int gymId, DateTime? datePurchase = null)
        {
            return new Equipment(equipmentName, gymId, datePurchase);
        }
    }
}
