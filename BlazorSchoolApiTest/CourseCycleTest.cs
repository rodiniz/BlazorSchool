// using AutoFixture;
// using BlazorSchoolApi;
// using BlazorSchoolApi.Data;
// using BlazorSchoolShared.Dto;
// using System.Net;
//
// namespace BlazorSchoolApiTest
// {
//     public class CourseCycleTest
//     {
//         private CustomWebApplicationFactory<Program> _factory;
//         private Fixture _fixture;
//         private HttpClient _client;
//
//
//         [SetUp]
//         public void Setup()
//         {
//             _fixture = new Fixture();
//             _client = new HttpClient();
//             _factory = new CustomWebApplicationFactory<Program>();
//             _client = _factory.CreateAuthenticatedClient();
//         }
//
//         [Test]
//         public async Task Should_ReturnCreatedWhenPostIsSuccessFull()
//         {
//             //Arrange
//
//             var teacher = _fixture.Build<ApplicationUser>()
//                 .Without(c => c.Id)
//                 .Create();
//
//             var course = _fixture.Build<Course>()
//                 .Without(c => c.Id)
//                 .Create();
//
//             _factory.SeedData(dbc =>
//             {
//                 dbc.Users.Add(teacher);
//                 dbc.Courses.Add(course);
//             });
//
//             var courseCycle = _fixture.Build<CourseCycleDto>()
//                 .With(c => c.TeacherId, teacher.Id)
//                 .With(c => c.CourseId, course.Id)
//                 .Create();
//
//             // Act
//             var created = await _client.PostAsJsonAsync("/CourseCycle", courseCycle);
//             Assert.That(created.StatusCode == HttpStatusCode.Created);
//         }
//
//         [Test]
//         public async Task ShouldReturnNotFoundWhenCourseCycleDosNotExistForGet()
//         {
//             //Act
//             var response = await _client.GetAsync("/CourseCycle/-1");
//
//             //Assert
//             Assert.That(response.StatusCode == HttpStatusCode.NotFound);
//         }
//
//         [Test]
//         public async Task ShouldReturnOkWhenDeleteIsSuccessFull()
//         {
//             //Arrange
//             var courseCycle = _fixture.Create<CourseCycle>();
//
//             _factory.SeedData(dbc => { dbc.CourseCycles.Add(courseCycle); });
//             //Act
//             var response = await _client.DeleteAsync($"/CourseCycle/{courseCycle.Id}");
//
//             //Assert
//             Assert.That(response.StatusCode == HttpStatusCode.OK);
//         }
//
//         [Test]
//         public async Task ShouldReturnOkWhenGetIsSuccessFull()
//         {
//             //Arrange
//             var teacher = _fixture.Build<ApplicationUser>()
//                 .Without(c => c.Id)
//                 .Create();
//
//             var course = _fixture.Build<Course>()
//                 .Without(c => c.Id)
//                 .Create();
//
//             _factory.SeedData(dbc =>
//             {
//                 dbc.Users.Add(teacher);
//                 dbc.Courses.Add(course);
//             });
//
//             var courseCycle = _fixture.Build<CourseCycle>()
//             
//                 .Create();
//             
//             _factory.SeedData(dbc => { dbc.CourseCycles.Add(courseCycle); });
//             
//             //Act
//             var response = await _client.GetFromJsonAsync<CourseCycleDto>($"/CourseCycle/{courseCycle.Id}");
//
//             //Assert
//             Assert.That(response is not null);
//             Assert.That(response?.TeacherId == courseCycle.TeacherId);
//             Assert.That(response?.CourseId == courseCycle.CourseId);
//             Assert.That(response?.Year == courseCycle.Year);
//         }
//
//         [Test]
//         public async Task ShouldReturnNotFoundWhenCourseCycleDosNotExistForDelete()
//         {
//             //Act
//             var response = await _client.DeleteAsync("/CourseCycle/-1");
//
//             //Assert
//             Assert.That(response.StatusCode == HttpStatusCode.NotFound);
//         }
//
//         [TearDown]
//         public void TearDown()
//         {
//             _factory.Dispose();
//             _client.Dispose();
//         }
//     }
// }