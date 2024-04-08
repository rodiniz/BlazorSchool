using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;

namespace BlazorSchool.Pages.CourseCycle;

public partial class CourseTeacherList{
    
    [Parameter]
    public List<CourseTeacherDto> CourseTeachers { get; set; }
}