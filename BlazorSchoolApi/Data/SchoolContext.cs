using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BlazorSchoolApi.Data
{
    public class SchoolContext:IdentityDbContext<IdentityUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CourseCycle> CourseCycles { get; set; }
        public DbSet<StudentTests> StudentTests { get; set; }
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }
    }
}
