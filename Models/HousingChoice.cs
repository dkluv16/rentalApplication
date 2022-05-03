using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class HousingChoice
    {
        [Key]
        public int housingChoiceId { get; set; }

        public int housingTypeId { get; set; }

        public HousingType HousingType { get; set; }

        public int numberHousing { get; set; }

        public int beddingTypeId { get; set; }

        public BeddingType BeddingType { get; set; }

        public int registerId { get; set; }

        public Register Register { get; set; }
    }
}
