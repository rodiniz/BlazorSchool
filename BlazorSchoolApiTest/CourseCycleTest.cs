

using AutoFixture;
using BlazorSchoolApi;
using BlazorSchoolApi.Data;
using BlazorSchoolShared.Dto;
using System.Net;

namespace BlazorSchoolApiTest
{
    public class CourseCycleTest
    {
        private CustomWebApplicationFactory<Program> _factory;
        private Fixture _fixture;
        private HttpClient _client;


        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _client = new HttpClient();
            _factory = new CustomWebApplicationFactory<Program>();
            _client = _factory.CreateAuthenticatedClient();
        }
        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            //arrange

            var teacher = _fixture.Build<ApplicationUser>()
                .Without(c => c.Id)
                .Create();

            var course = _fixture.Build<Course>()
                .Without(c => c.Id)
                .Create();

            _factory.SeedData(dbc =>
            {
                dbc.Users.Add(teacher);
                dbc.Courses.Add(course);
            });

            var courseCycle = _fixture.Build<CourseCycleDto>()
                .With(c => c.TeacherId, teacher.Id)
                .With(c => c.CourseId, course.Id)
                .Create();

            // Act
            var created = await _client.PostAsJsonAsync("/CourseCycle", courseCycle);
            Assert.That(created.StatusCode == HttpStatusCode.Created);

            var response = await _client.GetFromJsonAsync<List<CourseCycleDto>>("/CourseCycle");

            Assert.That(response is not null);

            //update
            var cc = response.Single(c => c.TeacherId == teacher.Id);
            cc.Year = DateTime.Now.Year;

            var updateRespone = await _client.PutAsJsonAsync<CourseCycleDto>($"/CourseCycle/{cc.Id}", cc);
            Assert.That(updateRespone.StatusCode == HttpStatusCode.OK);


            var deleteResponse = await _client.DeleteAsync($"/CourseCycle/{response[0].Id}");
            Assert.That(deleteResponse.StatusCode == HttpStatusCode.OK);


            _factory.SeedData(dbc =>
            {
                dbc.Users.Remove(teacher);
                dbc.Courses.Remove(course);
            });

        }

        [Test]
        public async Task ShouldReturnNotFoundWhenCourseCycleDosNotExistForGet()
        {
            //Act
            var response = await _client.GetAsync("/CourseCycle/-1");

            //Assert
            Assert.That(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public async Task ShouldReturnNotFoundWhenCourseCycleDosNotExistForDelete()
        {
            //Act
            var response = await _client.DeleteAsync("/CourseCycle/-1");

            //Assert
            Assert.That(response.StatusCode == HttpStatusCode.NotFound);
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

    }
}
