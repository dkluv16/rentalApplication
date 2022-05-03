using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CampChetekRental.Models
{
    public class UserRole
    {
        public int userRoleId { get; set; }

        public string userDescription { get; set; }
    }
}
