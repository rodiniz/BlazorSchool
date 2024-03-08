using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Services
{
    public class CourseService : ICrudService<CourseDto>
    {
        private readonly SchoolContext _context;
        private readonly IValidator<CourseDto> _validator;
        public CourseService(SchoolContext context, IValidator<CourseDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        public Task<IResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Create(CourseDto model)
        {
            var result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                return TypedResults.BadRequest(result.Errors);
            }
            await _context.Courses.AddAsync(new Course { Description = model.Description });
            await _context.SaveChangesAsync();
            return TypedResults.Created();
        }

        public async Task<IResult> Update(int id, CourseDto model)
        {
            var result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                return TypedResults.BadRequest(result.Errors);
            }
            await _context.Courses
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(b => b.Description, model.Description)

                );
            return TypedResults.Ok();
        }

        public Task<IResult> Delete(int idEntity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> GetAll()
        {
            var result = _context.Courses.OrderBy(c => c.Description).Select(c => new CourseDto
            {
                Id = c.Id,
                Description = c.Description
            });
            return Task.FromResult(Results.Ok(result));
        }

        public Task<IResult> GetPaged(TableStateDto tableStateDto)
        {
            throw new NotImplementedException();
        }
    }
}
