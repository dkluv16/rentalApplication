using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class MealChoice
    {
        [Key]
        public int mealChoiceId { get; set; }

        public int numberOfMeals { get; set; }

        public int mealTypeId { get; set; }
        public MealType MealType { get; set; }

        public int registerId { get; set; }

        public Register Register { get; set; }
    }
}
