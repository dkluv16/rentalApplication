using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class BeddingType
    {
        [Key]
        public int beddingTypeId { get; set; }

        public bool isBedding { get; set; }

        public int cost { get; set; }
    }
}
