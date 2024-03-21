using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorSchool.Pages.Students
{
    public partial class ListStudents
    {
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        public List<StudentDto>? Students { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            Students= await HttpClient.GetFromJsonAsync<List<StudentDto>>("Student");
        }
        public void NavidateToSave(string id)
        {
            Manager.NavigateTo($"/Student/Save/{id}");
        }

        public async Task Delete(string id)
        {
            var result = await DialogService.ShowMessageBox(
                "Warning", 
                "Are you sure you want to delete?",
                yesText:"Yes", noText:"No");
            if (result.HasValue)
            {
                await HttpClient.DeleteAsync($"Student/{id}");
                Students= await HttpClient.GetFromJsonAsync<List<StudentDto>>("Student");
            }    
        }
    }
}
