using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolApi.Services;
using BlazorSchoolShared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Routes
{
    public static class StudentRoutes
    {
        public static void AddStudentRoutes(this WebApplication app)
        {
            var group = app.MapGroup("/Student");
          

            group.MapPost("/",async (
                    [FromServices] ICrudService<StudentDto> service,
                    [FromBody] StudentDto studentDto) => await service.Create(studentDto))
                .WithOpenApi();

            group.MapGet("/", ([FromServices] ICrudService<StudentDto> studentService) => studentService.GetAll());
            group.MapGet("/{id}", async ([FromServices] ICrudService<StudentDto> studentService, int id) => await studentService.Get(id));
            group.MapDelete("/{id}", async ([FromServices] ICrudService<StudentDto> studentService, int id) => await studentService.Delete(id));
        }

    }
}
