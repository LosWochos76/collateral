namespace SeminarManager.Model
{
    public class Seminar : Entity
    {
        public string Name { get; set; }
        public string Extent { get; set; }
        public int TeacherID { get; set; }
    }
}
