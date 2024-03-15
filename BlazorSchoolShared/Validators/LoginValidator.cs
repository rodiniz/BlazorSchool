using FluentValidation;
using Shared;

namespace BlazorSchoolShared.Validators;

public class LoginValidator:AbstractValidator<LoginModel>

{
    public LoginValidator()
    {
        RuleFor(course => course.Email).NotNull().NotEmpty();
        RuleFor(course => course.Password).NotNull().NotEmpty();
        RuleFor(course => course.Email).EmailAddress();
    }
}