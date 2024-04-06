namespace BlazorSchoolShared.Dto;

public class CourseTeacherDto
{
    public int Id { get; set; }
    public int? CourseId { get; set; }
    public string? TeacherId { get; set; }

    public string CourseName { get; set; }
    
    public string TeacherName { get; set; }
}