namespace BlazorSchoolApi.Data
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public virtual CourseCycle CourseCycle { get; set; }
    }
}
