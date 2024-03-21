using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSchoolApi.Routes
{
    public static class UsersRoutes
    {

        public static void AddStudentRoutes(this WebApplication app)
        {
            var group = app.MapGroup("/Users").RequireAuthorization();
            group.MapPost("/",async (
                    [FromServices] ICrudService<StudentDto,int> service,
                    [FromBody] StudentDto studentDto) => await service.Create(studentDto))
                .WithOpenApi();

            group.MapGet("/", ([FromServices] ICrudService<StudentDto,int> studentService) => studentService.GetAll());
            group.MapGet("/{id}", async ([FromServices] ICrudService<StudentDto,int> studentService, int id) => await studentService.Get(id));
            group.MapPut("/{id}", async ([FromServices] ICrudService<StudentDto,int> studentService, int id, [FromBody] StudentDto courseDto) => await studentService.Update(id, courseDto));
            group.MapDelete("/{id}", async ([FromServices] ICrudService<StudentDto,int> studentService, int id) => await studentService.Delete(id));
        }

    }
}
