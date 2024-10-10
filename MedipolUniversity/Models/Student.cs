namespace MedipolUniversity.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        // Öğrencinin kayıtlı olduğu dersler, öğrenci birden fazla derslere kayıt olabilir bu yüzden bu kullanım
        public ICollection<Enrollment> Enrollments { get; set; }


    }
}
