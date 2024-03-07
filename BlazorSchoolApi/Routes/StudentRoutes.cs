using BlazorSchoolApi.Data;
using BlazorSchoolShared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSchoolApi.Routes
{
    public static class StudentRoutes
    {
        public static void AddStudentRoutes(this WebApplication app)
        {
            var group = app.MapGroup("/Student");
            const string roleName = "Student";

            group.MapPost("Add",async (
                    [FromServices] UserManager<IdentityUser> userManager,
                    [FromServices] RoleManager<IdentityRole> roleManager,
                    [FromServices] SchoolContext context,
                    [FromBody] StudentDto studentDto) =>
                {

                    var roleExists= await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole { Name = roleName });
                    }

                    var user = new IdentityUser
                    {
                        UserName = studentDto.Email, 
                        Email = studentDto.Email
                    };
                  
                    var result= await userManager.CreateAsync(user, studentDto.Password);

                    if (!result.Succeeded)
                    {
                        return Results.BadRequest(result.Errors);
                    }

                    var userCreated = await userManager.FindByEmailAsync(studentDto.Email);
                    if (userCreated == null)
                    {
                        return Results.BadRequest();
                    }
                    var student = new Student
                    {
                        Name = studentDto.Name,
                        Address = studentDto.Address,
                        BirthDate = studentDto.BirthDate,
                        UserId = userCreated.Id

                    };
                    await context.Students.AddAsync(student);
                    await userManager.AddToRoleAsync(user, roleName);
                    return Results.Created();
                })
                .WithOpenApi();

            group.MapGet("List", (SchoolContext context) =>
            {
                return context.Students.Select(c => new StudentDto
                {
                    Address = c.Address,
                    Name = c.Name,
                    BirthDate = c.BirthDate
                }).ToList();
            });
        }
       
    }
}
