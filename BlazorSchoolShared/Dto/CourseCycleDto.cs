namespace BlazorSchoolShared.Dto;

public class CourseCycleDto
{
    public int Id { get; set; }
    public  string? CourseName { get; set; }
    public  int? CourseId { get; set; }
    
    public string? TeacherId { get; set; }
    public string TeacherName { get; set; }
    public int? Year { get; set; }
}