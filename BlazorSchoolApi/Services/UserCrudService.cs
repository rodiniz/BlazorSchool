using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Services
{
    public class UserCrudService : ICrudService<UserDto,string>
    {
        
        private readonly SchoolContext _context;
        
        private readonly IUserService _userService;
        
        private readonly UserManager<ApplicationUser> _userManager;
        public UserCrudService(
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
            var user = await _userService.GetUserById(id);
            return user == null ? TypedResults.NotFound() :
                    TypedResults.Ok(new UserDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Address = user.Address,
                        BirthDate = user.BirthDate
                    });
        }

        public async Task<IResult> Create(UserDto model)
        {
           
            var id= await _userService.CreateUser( new ApplicationUser
            {
                Email = model.Email,
                Name = model.Name,
                Address = model.Address,
                UserName = model.Email
            },model.RoleName);
            if (string.IsNullOrEmpty(id))
            {
                return TypedResults.BadRequest();
            }
            return TypedResults.Created();
        }

        public async Task<IResult> Update(string id, UserDto model)
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
            var users = await (from u in  _context.Users
                join ur in _context.UserRoles on  u.Id equals ur.UserId
                join ro in _context.Roles on ur.RoleId equals ro.Id
                select new { u.Id, u.Email, u.Name,RoleName= ro.Name ,u.Address,u.BirthDate})
                .ToListAsync();
            return TypedResults.Ok(
                users.Select(c => 
                    new UserDto { 
                        Id = c.Id, 
                        Email = c.Email, 
                        Name = c.Name,
                        Address = c.Address,
                        BirthDate = c.BirthDate,
                        RoleName = c.RoleName
                    }));
        }

        public Task<IResult> GetPaged(TableStateDto tableStateDto)
        {
            return Task.FromResult<IResult>(TypedResults.Ok());
        }

       
    }
}
