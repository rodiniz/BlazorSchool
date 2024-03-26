

using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorSchool.Pages.Users
{
    
    public partial class SaveUser
    {
        [Parameter]
        public string? Id { get; set; }

        [Inject] 
        private  HttpClient? Client { get; set; }

        private UserDto? _userDto=new();
        public bool IsReadOnly()
        {
            return !string.IsNullOrEmpty(Id);
        }
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                _userDto = (await Client!.GetFromJsonAsync<UserDto>($"Users/{Id}"));
            }
        }
        protected async Task SubmitValidForm()
        {
            HttpResponseMessage message;
            if (!string.IsNullOrEmpty(Id))
            {
                message= await Client!.PutAsJsonAsync($"Users/{Id}", _userDto);
            }
            else
            {
                message=await Client!.PostAsJsonAsync($"/Users", _userDto);
            }

            if(message.IsSuccessStatusCode){
                Manager!.NavigateTo("/Users/List");
            }
            else{
                Snackbar.Add("Error saving  User", Severity.Error);
            }
        }
            
    }
}
