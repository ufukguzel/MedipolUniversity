using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedipolUniversity.Models;

namespace MedipolUniversity.Data
{
    public class SchoolContext : DbContext
    {
        //SchoolContext veritabanı bağlamını oluşturmuş ve Student, Enrollment ve Course tablolarına erişimi sağlayan DbSet özelliklerini tanımladım
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        //OnModelCreating metodu ile veritabanı tablolarının Course, Enrollment, Student => "Course", "Enrollment" ve "Student" adında tablolarla eşleşmesini sağladım.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");

        }
    }
}
