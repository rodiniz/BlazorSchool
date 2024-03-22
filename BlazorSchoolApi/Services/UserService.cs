using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<UserService> _logger;
    private readonly SchoolContext _context;
    public UserService(
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        ILogger<UserService> logger, SchoolContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
       
        _logger = logger;
        _context = context;
    }

    public async Task<string?> CreateUser(ApplicationUser user, string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
        }
        var strongPassword = PasswordHelper.GenerateStrongPassword(12);
        var result=await _userManager.CreateAsync(user,strongPassword);
        if (!result.Succeeded)
        {
            _logger.LogInformation($"Error creating user {result.Errors}" );
            return null;
        }
        //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var userCreated = await _userManager.FindByEmailAsync(user.Email);

        result=await _userManager.AddToRoleAsync(userCreated, roleName);
        if (!result.Succeeded)
        {
            _logger.LogInformation($"Error creating user {result.Errors}" );
            return null;
        }
        //await _emailSenderService.SendEmail(email, "Account created", $"Your account was created and your password is {strongPassword}");
        
        return userCreated?.Id;
    }

    public async Task<string?> GetUserEmail(string userId)
    {
        var userCreated = await _userManager.FindByIdAsync(userId);
        return userCreated.Email;
    }

    public async Task<ApplicationUser> GetUserById(string userId)
    {
        var userCreated = await _userManager.FindByIdAsync(userId);
        return userCreated;
    }

    public async Task<IResult> GetByRole(string role)
    {
            var users = await (from u in  _context.Users
                    join ur in _context.UserRoles on  u.Id equals ur.UserId
                    join ro in _context.Roles on ur.RoleId equals ro.Id
                    where ro.Name ==role
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
}