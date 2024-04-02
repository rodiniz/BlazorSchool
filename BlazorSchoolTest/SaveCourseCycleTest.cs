using AutoFixture;
using BlazorSchoolShared.Dto;
using RichardSzalay.MockHttp;
using System.Linq;

namespace BlazorSchoolTest
{
    public class SaveCourseCycleTest : BaseMudBlazorTest
    {
   [Fact]
        public void ShouldLoadInputsWithValues()
        {
            // Arrange
            var courseCycle = Fixture.Create<CourseCycleDto>();
            var courses = Fixture.CreateMany<CourseDto>().ToList();
            var teachers = Fixture.CreateMany<UserDto>().ToList();
            var id = Fixture.Create<int>();

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

         var content = cut.FindAll("td[data-label=Description]");

            for (var i = 0; i < content.Count; i++)
            {
                Assert.Equal(content[i].InnerHtml, courses[i].Description);
            }

        }
    }
}
