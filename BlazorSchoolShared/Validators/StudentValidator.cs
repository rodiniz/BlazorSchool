using BlazorSchoolShared.Dto;
using FluentValidation;

namespace BlazorSchoolShared.Validators
{
    public class StudentValidator :AbstractValidator<StudentDto>
    {
        public StudentValidator()
        {
            RuleFor(c=>c.Name).NotEmpty();
            RuleFor(c=>c.Email).NotEmpty().EmailAddress();
            RuleFor(c=>c.BirthDate).NotNull().LessThan(DateTime.UtcNow);
        }
    }
}
