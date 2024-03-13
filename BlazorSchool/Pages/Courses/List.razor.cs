using System.Net.Http.Json;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Components;

namespace BlazorSchool.Pages.Courses
{
    public partial class List
    {
        [Inject] private HttpClient HttpClient { get; set; }
        public List<CourseDto>? Courses { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Courses= await HttpClient.GetFromJsonAsync<List<CourseDto>>("Course");
        }
        public void NavidateToSave(int? id)
        {
            Manager.NavigateTo("/SaveCourse");
        }

        public void Delete(int id)
        {

        }
    }
}
