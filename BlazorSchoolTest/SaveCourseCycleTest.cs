using AutoFixture;
using BlazorSchoolShared.Dto;
using MudBlazor.Services;
using RichardSzalay.MockHttp;
using System.Linq;

namespace BlazorSchoolTest
{
    public class SaveCourseCycleTest : TestContext
    {
        private readonly Fixture _fixture = new();

        [Fact]
        public void ShouldLoadInputsWithValues()
        {
            // Arrange
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorizing();

            Services.AddMudServices();
            var courseCycle = _fixture.Create<CourseCycleDto>();
            var courses = _fixture.CreateMany<CourseDto>().ToList();
            var teachers = _fixture.CreateMany<UserDto>().ToList();
            var id = _fixture.Create<int>();

            courses[0].Id = courseCycle.CourseId!.Value;
            teachers[0].Id = courseCycle.TeacherId!;

            var mock = Services.AddMockHttpClient();
            mock.When($"/CourseCycle/{id}").RespondJson(courseCycle);
            mock.When($"/Course").RespondJson(courses);
            mock.When($"/Users/GetTeachers").RespondJson(teachers);

            JSInterop.SetupVoid("mudPopover.initialize", "mudblazor-main-content", 0);
            JSInterop.SetupVoid("mudPopover.connect", _ => true);
            JSInterop.SetupVoid("mudKeyInterceptor.connect", _ => true);



            var cut = RenderComponent<BlazorSchool.Pages.CourseCycle.SaveCourseCycle>(parameters => parameters
                .Add(p => p.Id, id)
            );

            // Assert that content of the paragraph shows counter at zero
            var content = cut.FindAll("td[data-label=Description]");

            for (var i = 0; i < content.Count; i++)
            {
                Assert.Equal(content[i].InnerHtml, courses[i].Description);
            }

        }
    }
}
