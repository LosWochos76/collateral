using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeminarManager.Model
{
    public class Person : Entity, IValidatableObject
    {
        public Person() 
        {
            Teaching = new List<Seminar>();
        }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public string EMail { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return Firstname + " " + Lastname;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Firstname.Equals(Lastname))
            {
                yield return new ValidationResult("Firstname and Lastname can not be equal!");
            }
        }

        public List<Seminar> Teaching { get; set; }
    }
}