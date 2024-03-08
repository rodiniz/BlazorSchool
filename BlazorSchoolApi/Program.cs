using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolApi.Routes;
using BlazorSchoolApi.Services;
using BlazorSchoolShared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddScoped<ICrudService<StudentDto>, StudentService>();

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

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) 
    .AllowCredentials()); 

app.Run();
