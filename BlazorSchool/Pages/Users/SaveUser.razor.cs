

using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorSchool.Pages.Users
{
    
    public partial class SaveUser
    {
        [Parameter]
        public string Id { get; set; }

        [Inject] 
        private  HttpClient _client { get; set; }
        
        [Inject] 
        protected  NavigationManager _Manager { get; set; }

        private UserDto _dto=new();
        public bool IsReadOnly()
        {
            return !string.IsNullOrEmpty(Id);
        }
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                _dto = (await _client.GetFromJsonAsync<UserDto>($"Users/{Id}"));
            }
        }
        protected async Task SubmitValidForm()
        {
            HttpResponseMessage message;
            if (!string.IsNullOrEmpty(Id))
            {
                message= await _client.PutAsJsonAsync($"Users/{Id}", _dto);
            }
            else
            {
                message=await _client.PostAsJsonAsync($"/Users", _dto);
            }

            if(message.IsSuccessStatusCode){
                _Manager.NavigateTo("/Users/List");
            }
            else{
                Snackbar.Add("Error saving  User", Severity.Error);
            }
        }
            
    }
}
