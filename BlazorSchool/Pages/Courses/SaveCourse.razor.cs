using BlazorSchoolShared.Dto;

namespace BlazorSchool.Pages.Courses
{
    public partial class SaveCourse
    {
        private CourseDto _course = new();
        private void SubmitValidForm()
            => Console.WriteLine("Form Submitted Successfully!");
    }
}
