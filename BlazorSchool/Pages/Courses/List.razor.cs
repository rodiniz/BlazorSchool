using BlazorSchoolShared.Dto;
using System.Net.Http.Json;

namespace BlazorSchool.Pages.Courses
{
    public partial class List
    {

        private List<CourseDto>? Courses { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Courses = await HttpClient!.GetFromJsonAsync<List<CourseDto>>("Course");
        }
        public void NavidateToSave(int? id)
        {
            Manager!.NavigateTo($"/Course/Save/{id}");
        }
        public async Task Delete(int id)
        {
            bool? result = await DialogService!.ShowMessageBox(
                "Warning",
                "Are you sure you want to delete?",
                yesText: "Yes", noText: "No");
            if (result.HasValue)
            {
                await HttpClient!.DeleteAsync($"Course/{id}");
                Courses = await HttpClient.GetFromJsonAsync<List<CourseDto>>("Course");
            }
        }
    }
}
