namespace BlazorSchoolShared.Dto;

public class EnrolmentDto
{
    public int Id { get; set; }
    public string StudentId { get; set; }
    public int CourseCycleId { get; set; }
    public string StudentName { get; set; }
    public string CourseCycleName { get; set; }
}