using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSchoolApi.Routes
{
    public static class CourseRoutes
    {
        public static void AddCourseRoutes(this WebApplication app)
        {
            var group = app.MapGroup("/Course").RequireAuthorization();
            group.MapPost("/", async (
                    [FromServices] ICrudService<CourseDto> service,
                    [FromBody] CourseDto courseDto) => await service.Create(courseDto))
                .WithOpenApi();

            group.MapGet("/", ([FromServices] ICrudService<CourseDto> courseService) => courseService.GetAll());
            group.MapGet("/{id}", async ([FromServices] ICrudService<CourseDto> studentService, int id) => await studentService.Get(id));
            group.MapPut("/{id}", async ([FromServices] ICrudService<CourseDto> courseService, int id, [FromBody] CourseDto courseDto) => await courseService.Update(id,courseDto));
            group.MapDelete("/{id}", async ([FromServices] ICrudService<CourseDto> courseService, int id) => await courseService.Delete(id));
        }
    }
}
