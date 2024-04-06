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

    private CourseTeacherDto CourseTeacher { get; set; } = new();
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    [Parameter] public int? Id { get; set; }



    protected override async Task OnInitializedAsync()
    {
        Teachers = await Client!.GetFromJsonAsync<List<UserDto>>($"Users/GetTeachers");
        CourseDtos = await Client!.GetFromJsonAsync<List<CourseDto>>($"Course");
    }

    void Submit()
    {
        MudDialog.Close(DialogResult.Ok(new CourseTeacherDto
        {
            CourseId = CourseTeacher.CourseId,
            TeacherId = CourseTeacher.TeacherId,
            TeacherName = Teachers!.SingleOrDefault(c => c.Id == CourseTeacher.TeacherId)?.Name,
            CourseName = CourseDtos!.SingleOrDefault(c => c.Id == CourseTeacher.CourseId)?.Description
        }));
    }
    void Cancel() => MudDialog.Cancel();
}