using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CampChetekRental.Models
{
    public class Register : IValidatableObject
    {
        [Key]

        public int registerId { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name.")]
        public string LastName { get; set; }

        public int GroupTypeId { get; set; }
        public GroupType GroupType { get; set; }

        [Range(10,400, ErrorMessage = "Groups can only be between 10 - 400.")]
        public int GroupNumber { get; set; }

        [Phone(ErrorMessage = "Please enter a Phone.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter an Address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter a City.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a State.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter a Zip.")]
        public int Zip { get; set; }

        public string FoodAllergies { get; set; }
 
        public bool Pet { get; set; }

        [Required]
        public DateTime event_start { get; set; }

        [Required]
        public DateTime event_end { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (event_end < event_start)
            {
                yield return
                  new ValidationResult(errorMessage: "Depart Date must be greater than Arrival Date",
                                       memberNames: new[] { "EndDate" });
            }
        }

    }
   
}
