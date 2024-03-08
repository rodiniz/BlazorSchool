using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSchoolApi.Routes
{
    public static class CourseRoutes
    {
        public static void AddCourseRoutes(this WebApplication app)
        {
            var group = app.MapGroup("/Course");
            group.MapPost("/", async (
                    [FromServices] ICrudService<CourseDto> service,
                    [FromBody] CourseDto courseDto) => await service.Create(courseDto))
                .WithOpenApi();

            group.MapGet("/", ([FromServices] ICrudService<CourseDto> studentService) => studentService.GetAll());
            group.MapGet("/{id}", async ([FromServices] ICrudService<CourseDto> studentService, int id) => await studentService.Get(id));
            group.MapDelete("/{id}", async ([FromServices] ICrudService<CourseDto> studentService, int id) => await studentService.Delete(id));
        }
    }
}
