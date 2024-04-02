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
builder.Services.AddSwaggerGen(c =>
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    }));
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
//Console.WriteLine(conn);
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(conn));

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(c =>
{

    c.Password.RequireDigit = false;
    c.Password.RequiredLength = 5;
    c.Password.RequireNonAlphanumeric = false;
    c.Password.RequiredUniqueChars = 1;
    c.Password.RequireUppercase = false;
    c.Password.RequireLowercase = false;

}).AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddEntityFrameworkStores<SchoolContext>();

builder.Services.AddCors();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddScoped<ICrudService<CourseDto, int>, CourseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICrudService<UserDto, string>, UserCrudService>();
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();
builder.Services.AddValidatorsFromAssemblyContaining<CourseValidator>();
builder.Services.AddScoped<ICrudService<CourseCycleDto, int>, CourseCycleService>();
var app = builder.Build();
//app.MapGroup("/identity").MapIdentityApi<ApplicationUser>();

app.UseSwagger();
app.UseSwaggerUI();

//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<SchoolContext>();
//    dataContext.Database.Migrate();
//}
app.UseHttpsRedirection();

app.AddUserRoutes();
app.AddGenericCrudRoutes<CourseDto>("Course");
app.AddGenericCrudRoutes<CourseCycleDto>("CourseCycle");
app.AddIdentityRoutes();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin()
    );


app.Run();
