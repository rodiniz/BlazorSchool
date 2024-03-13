using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSchoolApi.Routes
{
    public static class StudentRoutes
    {

        public static void AddStudentRoutes(this WebApplication app)
        {
            var group = app.MapGroup("/Student").RequireAuthorization();
            group.MapPost("/",async (
                    [FromServices] ICrudService<StudentDto> service,
                    [FromBody] StudentDto studentDto) => await service.Create(studentDto))
                .WithOpenApi();

            group.MapGet("/", ([FromServices] ICrudService<StudentDto> studentService) => studentService.GetAll());
            group.MapGet("/{id}", async ([FromServices] ICrudService<StudentDto> studentService, int id) => await studentService.Get(id));
            group.MapPut("/{id}", async ([FromServices] ICrudService<StudentDto> studentService, int id, [FromBody] StudentDto courseDto) => await studentService.Update(id, courseDto));
            group.MapDelete("/{id}", async ([FromServices] ICrudService<StudentDto> studentService, int id) => await studentService.Delete(id));
        }

    }
}
