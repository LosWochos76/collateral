using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeminarManager.Model;

namespace SeminarManager.MVC.ViewModel
{
    public class SeminarViewModel : Entity
    {
        public SeminarViewModel()
        {
            Attendees = new List<int>();
        }

        [Required]
        public string Name { get; set; }

        [Extent]
        public string Extent { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a Teacher for this seminar!")]
        public int Teacher { get; set; }

        public List<int> Attendees { get; set; }

        public static SeminarViewModel Convert(Seminar obj) 
        {
            return new SeminarViewModel()
            {
                ID = obj.ID,
                Name = obj.Name,
                Extent = obj.Extent,
                Teacher = obj.TeacherID
            };
        }

        public static Seminar Convert(SeminarViewModel obj)
        {
            return new Seminar()
            {
                ID = obj.ID,
                Name = obj.Name,
                Extent = obj.Extent,
                TeacherID = obj.Teacher
            };
        }
    }
}