using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;

namespace BlazorSchool.Pages.Students
{
    
    public partial class SaveStudent
    {
        [Parameter]
        public int? Id { get; set; }

        private StudentDto _student = new();

        [Inject] 
        private  HttpClient _client { get; set; }
        
        [Inject] 
        private  NavigationManager _Manager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                _student = (await _client.GetFromJsonAsync<StudentDto>($"Student/{Id.Value}"))!;
            }
        }

        private async Task SubmitValidForm()
        {
            if (Id.HasValue)
            {
                await _client.PutAsJsonAsync($"/Student/{Id.Value}", _student);
            }
            else
            {
               await _client.PostAsJsonAsync("/Student", _student);
            }
            _Manager.NavigateTo("/Students/List");
        }
            
    }
}
