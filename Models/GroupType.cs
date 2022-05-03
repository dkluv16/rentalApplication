using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class GroupType
    {
        [Key]
        public int GroupTypeId { get; set; }

        public string TypeName { get; set; }
    }
}
