using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSchoolApi.Routes
{
    public static class UsersRoutes
    {

        public static void AddUserRoutes(this WebApplication app)
        {
            var group = app.MapGroup("/Users").RequireAuthorization();
            group.MapPost("/", async (
                    [FromServices] ICrudService<UserDto, string> service,
                    [FromBody] UserDto studentDto) => await service.Create(studentDto))
                .WithOpenApi();

            group.MapGet("/", ([FromServices] ICrudService<UserDto, string> crudUserService) => crudUserService.GetAll());
            group.MapGet("/GetTeachers", ([FromServices] IUserService userService) => userService.GetByRole("Teacher"));
            group.MapGet("/GetStudents", ([FromServices] IUserService userService) => userService.GetByRole("Student"));
            group.MapGet("/{id}", async ([FromServices] ICrudService<UserDto, string> crudUserService, string id) => await crudUserService.Get(id));
            group.MapPut("/{id}", async ([FromServices] ICrudService<UserDto, string> crudUserService, string id, [FromBody] UserDto courseDto) => await crudUserService.Update(id, courseDto));
            group.MapDelete("/{id}", async ([FromServices] ICrudService<UserDto, string> studentService, string id) => await studentService.Delete(id));
        }

    }
}
