using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeminarManager.Model;

namespace SeminarManager.MVC.ViewModel
{
    public class PersonViewModel : Entity, IValidatableObject
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public string EMail { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return Firstname + " " + Lastname;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Firstname.Equals(Lastname))
                yield return new ValidationResult("Firstname and Lastname can not be equal!");

            if (IsAdmin && string.IsNullOrEmpty(Password))
                yield return new ValidationResult("If user is an admin she must have a password!");
        }

        public static Person Convert(PersonViewModel vm)
        {
            return new Person() 
            {
                ID = vm.ID,
                Firstname = vm.Firstname,
                Lastname = vm.Lastname,
                EMail = vm.EMail,
                IsAdmin = vm.IsAdmin,
                Password = vm.Password
            };
        }

        public static PersonViewModel Convert(Person p)
        {
            return new PersonViewModel()
            {
                ID = p.ID,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                EMail = p.EMail,
                IsAdmin = p.IsAdmin,
                Password = p.Password
            };
        }
    }
}