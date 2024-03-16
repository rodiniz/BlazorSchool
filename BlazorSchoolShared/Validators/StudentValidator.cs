using BlazorSchoolShared.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSchoolShared.Validators
{
    public class StudentValidator :AbstractValidator<StudentDto>
    {
        public StudentValidator()
        {
            RuleFor(c=>c.Name).NotEmpty();
            RuleFor(c=>c.Email).NotEmpty().EmailAddress();
            RuleFor(c=>c.BirthDate).NotNull().LessThan(DateTime.UtcNow);
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}
