using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Services
{
    public class StudentService : ICrudService<StudentDto>
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly SchoolContext _context;

        private readonly IEmailSenderService _emailSenderService;

        private const string LowerCase = "abcdefghijklmnopqursuvwxyz";
        private const string UpperCases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Numbers = "1234567890";
        private static readonly Random random = new();

        public StudentService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SchoolContext context,
            IEmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _emailSenderService = emailSenderService;
        }

        public async Task<IResult> Get(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(c => c.Id == id);
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
            const string roleName = "Student";
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            string strongPassword = GenerateStrongPassword(20);

            var result = await _userManager.CreateAsync(user, strongPassword);

            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Errors);
            }


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);



            var userCreated = await _userManager.FindByEmailAsync(model.Email);
            if (userCreated == null)
            {
                return TypedResults.BadRequest();
            }
            var student = new Student
            {
                Name = model.Name,
                Address = model.Address,
                BirthDate = model.BirthDate ?? DateTime.Now,
                UserId = userCreated.Id

            };
            await _emailSenderService.SendEmail(model.Email, "Account created", $"Your account was created and your password is{strongPassword}");

            await _context.Students.AddAsync(student);
            await _userManager.AddToRoleAsync(user, roleName);
            await _context.SaveChangesAsync();
            return TypedResults.Created();
        }

        public async Task<IResult> Update(int id, StudentDto model)
        {
            var affected = await _context.Students
                  .Where(b => b.Id == id)
                  .ExecuteUpdateAsync(setters => setters
                      .SetProperty(b => b.Name, model.Name)
                      .SetProperty(b => b.Address, model.Address)
                      .SetProperty(b => b.BirthDate, model.BirthDate)
                  );

            return affected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        }

        public async Task<IResult> Delete(int idEntity)
        {
            var affected = await _context.Students.Where(c => c.Id == idEntity).ExecuteDeleteAsync();
            return affected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        }

        public Task<IResult> GetAll()
        {
            var result = _context.Students.AsNoTracking().OrderBy(c => c.Name).Select(c => new StudentDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                BirthDate = c.BirthDate
            });
            return Task.FromResult(Results.Ok(result));
        }

        public Task<IResult> GetPaged(TableStateDto tableStateDto)
        {
            var result = _context.Students.AsNoTracking().OrderBy(c => c.Name).Select(c => new StudentDto
            {
                Name = c.Name,
                Address = c.Address,
                BirthDate = c.BirthDate
            });
            return Task.FromResult(Results.Ok(result));
        }

        public static string GenerateStrongPassword(int passwordSize)
        {
            if (passwordSize < 8)
                throw new ArgumentException("The password size must be equals or greater than 8.", nameof(passwordSize));

            var numberOfLowerCase = random.Next(1, passwordSize - 4);
            var numberOfUpperCase = random.Next(1, passwordSize - numberOfLowerCase - 2);
            var numberOfNumbers = passwordSize - numberOfLowerCase - numberOfUpperCase;

            var lowerCaseCharacters = GetRandomString(LowerCase, numberOfLowerCase);
            var upperCaseCharacters = GetRandomString(UpperCases, numberOfUpperCase);
            var numberCharacters = GetRandomString(Numbers, numberOfNumbers);

            var password = $"{lowerCaseCharacters}{upperCaseCharacters}{numberCharacters}";

            return new string(password.ToCharArray().OrderBy(_ => random.Next(2) % 2 == 0).ToArray());
        }

        private static string GetRandomString(string chars, int length)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
