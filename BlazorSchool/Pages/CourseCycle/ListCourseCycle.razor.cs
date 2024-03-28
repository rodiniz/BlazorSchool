using BlazorSchoolShared.Dto;
using System.Net.Http.Json;

namespace BlazorSchool.Pages.CourseCycle;

public partial class ListCourseCycle
{
    public List<CourseCycleDto>? CourseCycles { get; set; }

    public List<CourseDto>? Courses { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CourseCycles = await HttpClient!.GetFromJsonAsync<List<CourseCycleDto>>("CourseCycle");
    }
    public void NavidateToSave(int? id)
    {
        Manager!.NavigateTo($"/CourseCycle/Save/{id}");
    }

    public async Task Delete(int id)
    {
        bool? result = await DialogService!.ShowMessageBox(
            "Warning",
            "Are you sure you want to delete?",
            yesText: "Yes", noText: "No");
        if (result.HasValue)
        {
            await HttpClient!.DeleteAsync($"CourseCycle/{id}");
            CourseCycles = await HttpClient.GetFromJsonAsync<List<CourseCycleDto>>("CourseCycle");
        }
    }
}