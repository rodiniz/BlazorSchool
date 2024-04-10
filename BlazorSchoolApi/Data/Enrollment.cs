using BlazorSchoolApi.Interfaces;

namespace BlazorSchoolApi.Data
{
    public class Enrollment : IEntity
    {
        public int Id { get; set; }
        public ApplicationUser Student { get; set; }
        public CourseCycle CourseCycle { get; set; }
        public int CourseCycleId { get; set; }
        public string StudentId { get; set; }
    }
}
