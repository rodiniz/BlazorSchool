using BlazorSchoolShared.Dto;
using FluentValidation;

namespace BlazorSchoolShared.Validators;

public class CourseCycleValidator : AbstractValidator<CourseCycleDto>
{
    public CourseCycleValidator()
    {
        RuleFor(course => course.Year).NotNull().NotEqual(0);
        RuleFor(c => c.CourseTeachers)
            .Must(list => list != null && list.Count != 0)
            .WithMessage("Cycle must have at least one Course/Teacher");
    }
}