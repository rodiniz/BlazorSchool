using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorSchool.Pages.Enrolment;

public partial class ListEnrolments
{
    public List<CourseCycleDto>? CourseCycleDtos { get; set; } = [];

    public List<UserDto>? Students { get; set; } = [];

    public CourseCycleDto? CourseCycle { get; set; } = new();

    public UserDto? Student { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        CourseCycleDtos = await HttpClient.GetFromJsonAsync<List<CourseCycleDto>>("/CourseCycle");
        Students = await HttpClient.GetFromJsonAsync<List<UserDto>>("/User/GetStudents");
    }

    private async Task SubmitValidForm(EditContext arg)
    {
        var message = await HttpClient!.PostAsJsonAsync("/CourseCycle", CourseCycle);
        if (message.IsSuccessStatusCode)
        {
            Snackbar.Add("Enrolment saved", Severity.Success);
        }
        else
        {
            Snackbar.Add("Error saving Enrolment", Severity.Error);
        }
        CourseCycle = new();
    }
}