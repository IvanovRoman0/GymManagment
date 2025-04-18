using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Core.DTOs
{
    public class EquipmentDto
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public int GymId { get; set; }
        public DateTime? DatePurchase { get; set; }
    }
}
