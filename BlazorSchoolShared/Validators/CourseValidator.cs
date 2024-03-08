using FluentValidation;

namespace BlazorSchoolShared.Validators
{
    public class CourseValidator:AbstractValidator<CourseDto>
    {
        public CourseValidator()
        {
            RuleFor(course => course.Description).NotNull();
        }
    }
}
