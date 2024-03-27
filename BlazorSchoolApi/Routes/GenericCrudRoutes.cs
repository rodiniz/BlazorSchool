using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSchoolApi.Routes
{
    public static class GenericCrudRoutes
    {
        public static void AddGenericCrudRoutes<T>(this WebApplication app,string controllerName)
        { 
            var group = app.MapGroup($"/{controllerName}").RequireAuthorization();
            group.MapPost("/", async (
                    [FromServices] ICrudService<T,int> service,
                    [FromBody] T courseDto) => await service.Create(courseDto))
                .WithOpenApi();

            group.MapGet("/", ([FromServices] ICrudService<T,int> crudService) => crudService.GetAll());
            group.MapGet("/{id}", async ([FromServices] ICrudService<T,int> crudService, int id) => await crudService.Get(id));
            group.MapPut("/{id}", async ([FromServices] ICrudService<T,int> crudService, int id, [FromBody] T courseDto) => await crudService.Update(id,courseDto));
            group.MapDelete("/{id}", async ([FromServices] ICrudService<T,int> crudService, int id) => await crudService.Delete(id));
        }
    }
}
