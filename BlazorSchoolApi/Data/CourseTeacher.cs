namespace BlazorSchoolApi.Data;

public class CourseTeacher
{
    public int Id { get; set; }
    public virtual Course Course { get; set; }
    public int CourseId { get; set; }
    public virtual ApplicationUser Teacher { get; set; }
    public string TeacherId { get; set; }
}