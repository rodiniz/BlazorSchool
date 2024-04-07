using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorSchool.Pages.CourseCycle;

public partial class ListCourseCycle
{
    private List<CourseCycleDto>? Courses { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Courses = await HttpClient!.GetFromJsonAsync<List<CourseCycleDto>>("CourseCycle");
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
            await HttpClient!.DeleteAsync($"Course/{id}");
            Courses = await HttpClient.GetFromJsonAsync<List<CourseCycleDto>>("CourseCycle");
        }
    }
  
}