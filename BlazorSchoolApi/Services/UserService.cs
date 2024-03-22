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
    
    private static readonly Random Random = new();
    private const string LowerCase = "abcdefghijklmnopqursuvwxyz";
    private const string UpperCases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Numbers = "1234567890";
    

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
        var strongPassword = GenerateStrongPassword(12);
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

    private static string GenerateStrongPassword(int passwordSize)
    {
        if (passwordSize < 8)
            throw new ArgumentException("The password size must be equals or greater than 8.", nameof(passwordSize));

        var numberOfLowerCase = Random.Next(1, passwordSize - 4);
        var numberOfUpperCase = Random.Next(1, passwordSize - numberOfLowerCase - 2);
        var numberOfNumbers = passwordSize - numberOfLowerCase - numberOfUpperCase;

        var lowerCaseCharacters = GetRandomString(LowerCase, numberOfLowerCase);
        var upperCaseCharacters = GetRandomString(UpperCases, numberOfUpperCase);
        var numberCharacters = GetRandomString(Numbers, numberOfNumbers);

        var password = $"{lowerCaseCharacters}{upperCaseCharacters}{numberCharacters}";

        return new string(password.ToCharArray().OrderBy(_ => Random.Next(2) % 2 == 0).ToArray());
    }

    private static string GetRandomString(string chars, int length)
    {
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}