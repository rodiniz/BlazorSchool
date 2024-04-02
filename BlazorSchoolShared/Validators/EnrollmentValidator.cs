using BlazorSchoolShared.Dto;
using FluentValidation;

namespace BlazorSchoolShared.Validators;

public class EnrollmentValidator:AbstractValidator<EnrolmentDto>
{
    public EnrollmentValidator()
    {
        RuleFor(c => c.StudentId).NotNull().NotEmpty();
        RuleFor(c => c.CourseCycleId).NotEqual(0);
    }
}