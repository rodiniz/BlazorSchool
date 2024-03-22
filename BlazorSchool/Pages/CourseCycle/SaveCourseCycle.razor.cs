using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using MudBlazor;

namespace BlazorSchool.Pages.CourseCycle;

public partial class SaveCourseCycle
{
    private List<CourseDto> CourseDtos { get; set; }
    private List<UserDto>  Teachers { get; set; }
    public SaveCourseCycle() : base("CourseCycle")
    {
    }

    protected override async Task OnInitializedAsync()
    {
        CourseDtos= await _client.GetFromJsonAsync<List<CourseDto>>($"Course");
        Teachers= await _client.GetFromJsonAsync<List<UserDto>>($"Course");
    }

    private async Task SubmitValidForm()
    {
        var success=await  Save();
        if(success){
            _Manager.NavigateTo("/CourseCycle/List");
        }
        else{
            Snackbar.Add("Error saving  Course", Severity.Error);
        }
            
    }
}