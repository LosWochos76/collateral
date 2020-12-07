using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarManager.Model
{
    public class Seminar : Entity
    {
        public Seminar()
        {
            AttendeeIDs = new List<int>();
        }

        [Required]
        public string Name { get; set; }

        [Extent]
        public string Extent { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a Teacher for this seminar!")]
        public int TeacherID { get; set; }

        public List<int> AttendeeIDs { get; set; }
    }
}
