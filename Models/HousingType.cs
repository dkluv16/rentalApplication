using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class HousingType
    {
        [Key]
        public int housingTypeId { get; set; }

        public string building { get; set; }

        public string buildingDescription { get; set; }

        public int cost { get; set; }

        public string type { get; set; }

        public bool isActive { get; set; }

        public DateTime dateUpdated { get; set; }


    }
}
