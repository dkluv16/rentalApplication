using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class ProgramChoice
    {
        [Key]
        public int programChoiceId { get; set; }

        public int activityTypeId { get; set; }

        public ActivityType ActivityType { get; set; }

        public int numberParticipating { get; set; }

        public int registerId { get; set; }

        public Register Register { get; set; }

    }
}
