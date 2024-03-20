using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSchoolApi.Routes;

public static class TeacherRoutes
{
    public static void AddTeacherRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/Teacher").RequireAuthorization();
        group.MapPost("/",async (
                [FromServices] ICrudService<TeacherDto,string> service,
                [FromBody] TeacherDto teacherDto) => await service.Create(teacherDto))
            .WithOpenApi();

        group.MapGet("/", ([FromServices] ICrudService<TeacherDto,string> teacherService) => teacherService.GetAll());
        group.MapGet("/{id}", async ([FromServices] ICrudService<TeacherDto,string> teacherService, string id) => await teacherService.Get(id));
        group.MapPut("/{id}", async ([FromServices] ICrudService<TeacherDto,string> teacherService, string id, [FromBody] TeacherDto teacherDto) => await teacherService.Update(id, teacherDto));
        group.MapDelete("/{id}", async ([FromServices] ICrudService<TeacherDto,string> teacherService, string id) => await teacherService.Delete(id));
    }
}