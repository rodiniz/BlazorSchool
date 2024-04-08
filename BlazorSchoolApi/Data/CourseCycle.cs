using BlazorSchoolApi.Interfaces;

namespace BlazorSchoolApi.Data
{
    public class CourseCycle : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public int Year { get; set; }

        public List<CourseTeacher> CourseTeachers { get; set; }
    }
}
