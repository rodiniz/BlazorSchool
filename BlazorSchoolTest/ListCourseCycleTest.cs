using AutoFixture;
using BlazorSchoolShared.Dto;
using MudBlazor.Services;
using RichardSzalay.MockHttp;
using System.Linq;

namespace BlazorSchoolTest
{
    /// <summary>
    /// These tests are written entirely in C#.
    /// Learn more at https://bunit.dev/docs/getting-started/writing-tests.html#creating-basic-tests-in-cs-files
    /// </summary>
    public class ListCourseCycleTest : BaseMudBlazorTest
    {
        private Fixture? _fixture = new Fixture();

        [Fact]
        public void ShouldLoadGridWithCourses()
        {
            // Arrange
           
            var courses = _fixture.CreateMany<CourseCycleDto>().ToList();
            var mock = Services.AddMockHttpClient();
            mock.When("/CourseCycle").RespondJson(courses);

            var cut = RenderComponent<BlazorSchool.Pages.Courses.List>();

            // Assert that content of the paragraph shows counter at zero
            var courseName = cut.FindAll("td[data-label=CourseName]");
            var year = cut.FindAll("td[data-label=Year]");
            var teacherName = cut.FindAll("td[data-label=TeacherName]");

            for (var i = 0; i < courseName.Count; i++)
            {
                Assert.Equal(courseName[i].InnerHtml, courses[i].CourseName);
                Assert.Equal(year[i].InnerHtml, courses[i].Year.Value.ToString());
                Assert.Equal(teacherName[i].InnerHtml, courses[i].TeacherName);
            }

        }


    }
}
