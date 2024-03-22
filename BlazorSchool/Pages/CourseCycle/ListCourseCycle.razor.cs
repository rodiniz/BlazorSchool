using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorSchool.Pages.CourseCycle;

public partial class ListCourseCycle
{
    public List<CourseCycleDto> CourseCycles { get; set; }
    [Inject] private HttpClient? HttpClient { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    
    [Inject] private NavigationManager? Manager { get; set; }
    public List<CourseDto>? Courses { get; set; }
        
    protected override async Task OnInitializedAsync()
    {
        CourseCycles= await HttpClient.GetFromJsonAsync<List<CourseCycleDto>>("CourseCycle");
    }
    public void NavidateToSave(int? id)
    {
        Manager.NavigateTo($"/CourseCycle/Save/{id}");
    }

    public async Task Delete(int id)
    {
        bool? result = await DialogService.ShowMessageBox(
            "Warning", 
            "Are you sure you want to delete?",
            yesText:"Yes", noText:"No");
        if (result.HasValue)
        {
            await HttpClient.DeleteAsync($"CourseCycle/{id}");
            CourseCycles= await HttpClient.GetFromJsonAsync<List<CourseCycleDto>>("Course");
        }    
    }
}