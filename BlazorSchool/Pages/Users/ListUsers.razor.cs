using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorSchool.Pages.Users
{
    public partial class ListUsers
    {
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        public List<UserDto>? Students { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            Students= await HttpClient.GetFromJsonAsync<List<UserDto>>("Users");
        }
        public void NavidateToSave(string id)
        {
            Manager.NavigateTo($"/Users/Save/{id}");
        }

        public async Task Delete(string id)
        {
            var result = await DialogService.ShowMessageBox(
                "Warning", 
                "Are you sure you want to delete?",
                yesText:"Yes", noText:"No");
            if (result.HasValue)
            {
                await HttpClient.DeleteAsync($"Users/{id}");
                Students= await HttpClient.GetFromJsonAsync<List<UserDto>>("Users");
            }    
        }
    }
}
