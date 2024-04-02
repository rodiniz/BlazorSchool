using BlazorSchoolApi.Interfaces;

namespace BlazorSchoolApi.Data
{
    public class Enrollment : IEntity
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public virtual List<CourseCycle> CourseCycle { get; set; }
    }
}
