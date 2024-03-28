using BlazorSchoolShared.Dto;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorSchool.Pages.CourseCycle;

public partial class SaveCourseCycle
{
    private List<CourseDto>? CourseDtos { get; set; } = new List<CourseDto>();
    private List<UserDto>? Teachers { get; set; } = new List<UserDto>();
    public SaveCourseCycle() : base("CourseCycle")
    {
    }

    protected override async Task OnInitializedAsync()
    {
        CourseDtos = await Client!.GetFromJsonAsync<List<CourseDto>>($"Course");
        Teachers = await Client!.GetFromJsonAsync<List<UserDto>>($"Users/GetTeachers");
        await base.OnInitializedAsync();
    }

    private async Task SubmitValidForm()
    {
        var success = await Save();
        if (success)
        {
            Manager!.NavigateTo($"/{Url}/List");
        }
        else
        {
            Snackbar.Add("Error saving  Course", Severity.Error);
        }

    }
}