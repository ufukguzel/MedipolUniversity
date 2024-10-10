using Microsoft.Build.Framework;

namespace MedipolUniversity.Models
{
    public class Course
    {
        [Required]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
