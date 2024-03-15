using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;

namespace BlazorSchool.Pages.Courses
{
    
    public partial class SaveCourse
    {
        [Parameter]
        public int? Id { get; set; }
        private CourseDto _course = new();
        [Inject] private  HttpClient _client { get; set; }
        
        [Inject] private  NavigationManager _Manager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                _course = (await _client.GetFromJsonAsync<CourseDto>($"Course/{Id.Value}"))!;
            }
        }

        private async Task SubmitValidForm()
        {
            if (Id.HasValue)
            {
                await _client.PutAsJsonAsync($"/Course/{Id.Value}", _course);
            }
            else
            {
               await _client.PostAsJsonAsync("/Course", _course);
            }
            _Manager.NavigateTo("/Course/List");
        }
            
    }
}
