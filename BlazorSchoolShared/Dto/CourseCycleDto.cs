namespace BlazorSchoolShared.Dto;

public class CourseCycleDto
{
    public int? Id { get; set; }
    public int? Year { get; set; }

    public List<CourseTeacherDto> CourseTeachers { get; set; } = new();
    public string Description { get; set; }

    public bool IsActive { get; set; }
}