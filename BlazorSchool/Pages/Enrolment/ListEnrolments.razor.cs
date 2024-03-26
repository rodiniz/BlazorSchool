using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorSchool.Pages.Enrolment;

public partial class ListEnrolments
{

    public List<EnrolmentDto>? EnrolmentDtos { get; set; } = new List<EnrolmentDto>();
    [Inject] private HttpClient? HttpClient { get; set; }
    [Inject] private IDialogService? DialogService { get; set; }
    
    [Inject] private NavigationManager? Manager { get; set; }
    public List<CourseDto>? Courses { get; set; }
        
    protected override async Task OnInitializedAsync()
    {
        EnrolmentDtos= await HttpClient!.GetFromJsonAsync<List<EnrolmentDto>>("Enrolment");
    }
    public void NavidateToSave(int? id)
    {
        Manager!.NavigateTo($"/Enrolment/Save/{id}");
    }

    public async Task Delete(int id)
    {
        bool? result = await DialogService!.ShowMessageBox(
            "Warning", 
            "Are you sure you want to delete?",
            yesText:"Yes", noText:"No");
        if (result.HasValue)
        {
            await HttpClient?.DeleteAsync($"Enrolment/{id}")!;
            EnrolmentDtos= (await HttpClient.GetFromJsonAsync<List<EnrolmentDto>>("Enrolment"))!;
        }    
    }
}