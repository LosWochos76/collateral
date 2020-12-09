using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeminarManager.MVC.ViewModel
{
    public class ExtentAttribute : ValidationAttribute
    {
        private string error_message = "Extent needs to reflect the contact hours for both lectures and exercises, e.g. 2L3E.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var seminar = (SeminarViewModel)validationContext.ObjectInstance;
            var extent = seminar.Extent;

            if (extent == null)
                return new ValidationResult(error_message);

            var rgx = new Regex(@"^([0-9]+[EL])+$");
            var result = rgx.Matches(extent);
            
            if (result == null || result.Count == 0)
                return new ValidationResult(error_message);

            return ValidationResult.Success;
        }
    }
}
