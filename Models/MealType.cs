using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class MealType
    {
        [Key]
        public int mealTypeId { get; set; }
        public string mealDescription { get; set; }
        public int cost { get; set; }
        public string type { get; set; }
        public bool isActive { get; set; }
        public DateTime dateUpdated { get; set; }
    }
}
