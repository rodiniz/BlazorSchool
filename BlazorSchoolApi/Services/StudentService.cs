using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Services
{
    public class StudentService : ICrudService<StudentDto,string>
    {
        
        private readonly SchoolContext _context;
        
        private readonly IUserService _userService;
        
        private readonly UserManager<ApplicationUser> _userManager;
        public StudentService(
            SchoolContext context,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
        
            _context = context;
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IResult> Get(string id)
        {
            var student = await _userService.GetUserById(id);
            return student == null ? TypedResults.NotFound() :
                    TypedResults.Ok(new StudentDto
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Address = student.Address,
                        BirthDate = student.BirthDate
                    });
        }

        public async Task<IResult> Create(StudentDto model)
        {
           
            var id= await _userService.CreateUser( new ApplicationUser
            {
                Email = model.Email,
                Name = model.Name,
                Address = model.Address
            },"Student");
            if (string.IsNullOrEmpty(id))
            {
                return TypedResults.BadRequest();
            }
            return TypedResults.Created();
        }

        public async Task<IResult> Update(string id, StudentDto model)
        {
            var user = new ApplicationUser
            {
                Id = id,
                UserName = model.Name,
                Email = model.Email
            };
            var result= await _userManager.UpdateAsync(user);
            return result.Succeeded ? TypedResults.Ok() : TypedResults.BadRequest(result.Errors);
        }

        public async Task<IResult> Delete(string idEntity)
        {
            var user = new ApplicationUser
            {
                Id = idEntity
            };
            var result= await _userManager.DeleteAsync(user);
            return result.Succeeded ? TypedResults.Ok() : TypedResults.BadRequest(result.Errors);
        }

        public async Task<IResult> GetAll()
        {
            var users = await _userManager.GetUsersInRoleAsync("Teacher");
            return TypedResults.Ok(
                users.Select(c => 
                    new TeacherDto { 
                        Id = c.Id, 
                        Email = c.Email, 
                        Name = c.Name,
                        Address = c.Address,
                        BirthDate = c.BirthDate
                    }));
        }

        public Task<IResult> GetPaged(TableStateDto tableStateDto)
        {
            return Task.FromResult<IResult>(TypedResults.Ok());
        }

       
    }
}
