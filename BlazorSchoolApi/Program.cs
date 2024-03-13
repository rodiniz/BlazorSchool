using BlazorSchoolApi;
using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolApi.Routes;
using BlazorSchoolApi.Services;
using BlazorSchoolShared.Dto;
using BlazorSchoolShared.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    }));

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>(c =>
{

    c.Password.RequireDigit = false;
    c.Password.RequiredLength = 5;
    c.Password.RequireNonAlphanumeric = false;
    c.Password.RequiredUniqueChars = 1;
    c.Password.RequireUppercase = false;
    c.Password.RequireLowercase = false;

}).AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddUserManager<UserManager<IdentityUser>>()
    .AddEntityFrameworkStores<SchoolContext>();

builder.Services.AddCors();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddScoped<ICrudService<StudentDto>, StudentService>();
builder.Services.AddScoped<ICrudService<CourseDto>, CourseService>();
builder.Services.AddValidatorsFromAssemblyContaining<CourseValidator>();
var app = builder.Build();
app.MapGroup("/identity").MapIdentityApi<IdentityUser>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddStudentRoutes();
app.AddCourseRoutes();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) 
    .AllowCredentials());


app.Run();
