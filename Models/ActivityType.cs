using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class ActivityType
    {
        [Key]

        public int activityTypeId { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int timeOfYearId { get; set; }

        public TimeOfYear TimeOfYear {get; set;}

        public int cost { get; set; }

        public bool isActive { get; set; }

        public DateTime dateUpdated { get; set; }


    }
}
