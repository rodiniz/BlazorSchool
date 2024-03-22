using BlazorSchoolShared.Dto;
using FluentValidation;

namespace BlazorSchoolShared.Validators;

public class CourseCycleValidator:AbstractValidator<CourseCycleDto>
{
    public CourseCycleValidator()
    {
        RuleFor(course => course.CourseId).NotNull().NotEqual(0);
        RuleFor(course => course.TeacherId).NotNull().NotEmpty();
        RuleFor(course => course.Year).NotNull().NotEqual(0);
    }
}