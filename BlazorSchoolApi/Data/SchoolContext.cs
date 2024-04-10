using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BlazorSchoolApi.Data
{
    public class SchoolContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CourseCycle> CourseCycles { get; set; }
        public DbSet<StudentTests> StudentTests { get; set; }
        public DbSet<CourseTeacher> CourseTeachers { get; set; }
        public SchoolContext()
        {

        }
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

    }
}
