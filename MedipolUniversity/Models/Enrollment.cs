using System.ComponentModel.DataAnnotations;

namespace MedipolUniversity.Models
{
    //enumeration veri tipi
    public enum Grade
    {
        A, B, C, D, F
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
        [Required]
        public Course Course { get; set; }
        public Student Student { get; set; }

    }
}
