using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorSchool.Pages.CourseCycle;

public partial class CourseTeacherDialog
{
    [Inject] public HttpClient Client { get; set; }
    private List<UserDto>? Teachers { get; set; } = new List<UserDto>();
    private List<CourseDto>? CourseDtos { get; set; } = new List<CourseDto>();

    public CourseTeacherDto CourseTeacher { get; set; } = new();
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }



    protected override async Task OnInitializedAsync()
    {
        Teachers = await Client!.GetFromJsonAsync<List<UserDto>>($"Users/GetTeachers");
        CourseDtos = await Client!.GetFromJsonAsync<List<CourseDto>>($"Course");
    }

    void Submit()
    {
        CourseTeacher.CourseId = CourseTeacher.CourseId;
        CourseTeacher.TeacherId = CourseTeacher.TeacherId;
        CourseTeacher.TeacherName = Teachers!.SingleOrDefault(c => c.Id == CourseTeacher.TeacherId)?.Name;
        CourseTeacher.CourseName = CourseDtos!.SingleOrDefault(c => c.Id == CourseTeacher.CourseId)?.Description;

        MudDialog.Close(DialogResult.Ok(CourseTeacher));
    }
    void Cancel() => MudDialog.Cancel();
}