using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeminarManager.Model
{
    public class Person : Entity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return Firstname + " " + Lastname;
        }
    }
}