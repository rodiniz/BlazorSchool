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
    public class ListCourseTest : TestContext
    {
        private Fixture? _fixture = new Fixture();

        [Fact]
        public void CounterStartsAtZero()
        {
            // Arrange
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorizing();

            Services.AddMudServices();
            var courses = _fixture.CreateMany<CourseDto>().ToList();
            var mock = Services.AddMockHttpClient();
            mock.When("/Course").RespondJson(courses);

            var cut = RenderComponent<BlazorSchool.Pages.Courses.List>();

            // Assert that content of the paragraph shows counter at zero
            var content = cut.FindAll("td[data-label=Description]");

            for (var i = 0; i < content.Count; i++)
            {
                Assert.Equal(content[i].InnerHtml, courses[i].Description);
            }

        }


    }
}
