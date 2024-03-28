using MudBlazor;

namespace BlazorSchool.Pages.Courses
{

    public partial class SaveCourse
    {
        public SaveCourse() : base("Course")
        {

        }


        private async Task SubmitValidForm()
        {
            var success = await Save();
            if (success)
            {
                Manager!.NavigateTo($"/{Url}/List");
            }
            else
            {
                Snackbar.Add("Error saving  Course", Severity.Error);
            }

        }

    }
}
