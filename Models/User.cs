using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class User
    {
        public int userId { get; set; }

        [Required(ErrorMessage = "Please enter a First Name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a Last Name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a Password.")]
        
        public string Password { get; set; }
        [DataType(DataType.Password)]

        public string Salt { get; set; }

        public string Hash { get; set; }

        public string UserPassword { get; set; }

        public string UserEmail { get; set; }

        public int userRoleId { get; set; }

        public UserRole UserRole { get; set; }
    }
}
