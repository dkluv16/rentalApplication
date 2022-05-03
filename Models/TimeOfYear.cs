using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class TimeOfYear
    {
        [Key]

        public int timeOfYearId { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public string season { get; set; }
    }
}
