using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorSchool.Pages.Teachers;

public partial class ListTeachers
{
    [Inject] private HttpClient HttpClient { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    public List<TeacherDto>? Teachers { get; set; }
        
    protected override async Task OnInitializedAsync()
    {
        Teachers= await HttpClient.GetFromJsonAsync<List<TeacherDto>>("Teacher");
    }
    public void NavidateToSave(string? id)
    {
        Manager.NavigateTo($"/Teachers/Save/{id}");
    }

    public async Task Delete(string id)
    {
        bool? result = await DialogService.ShowMessageBox(
            "Warning", 
            "Are you sure you want to delete?",
            yesText:"Yes", noText:"No");
        if (result.HasValue)
        {
            await HttpClient.DeleteAsync($"Teachers/{id}");
            Teachers= await HttpClient.GetFromJsonAsync<List<TeacherDto>>("Teacher");
        }    
    }
}