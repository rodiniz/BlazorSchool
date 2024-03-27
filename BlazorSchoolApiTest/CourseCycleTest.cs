

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
            // Assuming you have a CustomWebApplicationFactory class to create the client
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

            var response = await _client.GetFromJsonAsync<IEnumerable<CourseCycleDto>>("/CourseCycle");

            // Assert

            Assert.That(response is not null);
            Assert.That(response.Count() == 1);
        }
    }
}
