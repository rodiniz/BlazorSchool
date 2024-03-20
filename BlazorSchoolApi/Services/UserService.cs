using BlazorSchoolApi.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlazorSchoolApi.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailSenderService _emailSenderService;
    private readonly ILogger<UserService> _logger;
    
    private static readonly Random Random = new();
    private const string LowerCase = "abcdefghijklmnopqursuvwxyz";
    private const string UpperCases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Numbers = "1234567890";
    

    public UserService(
        UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        IEmailSenderService emailSenderService, ILogger<UserService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailSenderService = emailSenderService;
        _logger = logger;
    }

    public async Task<string?> CreateUser(string email,string userName, string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
        }

        var user = new IdentityUser{Email = email, UserName = userName};
        var strongPassword = GenerateStrongPassword(12);
        var result=await _userManager.CreateAsync(user,strongPassword);
        if (!result.Succeeded)
        {
            _logger.LogInformation($"Error creating user {result.Errors}" );
            return null;
        }
        //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var userCreated = await _userManager.FindByEmailAsync(email);

        result=await _userManager.AddToRoleAsync(userCreated, roleName);
        if (!result.Succeeded)
        {
            _logger.LogInformation($"Error creating user {result.Errors}" );
            return null;
        }
        //await _emailSenderService.SendEmail(email, "Account created", $"Your account was created and your password is {strongPassword}");
        
        return userCreated?.Id;
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