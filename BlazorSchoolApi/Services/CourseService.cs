using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;

namespace BlazorSchoolApi.Services
{
    public class CourseService:ICrudService<CourseDto>
    {
        private readonly SchoolContext _context;

        public CourseService(SchoolContext context)
        {
            _context = context;
        }

        public Task<IResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Create(CourseDto model)
        {
           await  _context.Courses.AddAsync(new Course{  Description = model.Description});
           await _context.SaveChangesAsync();
           return TypedResults.Created();
        }

        public Task<IResult> Update(int id, CourseDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Delete(int idEntity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IResult> GetPaged(TableStateDto tableStateDto)
        {
            throw new NotImplementedException();
        }
    }
}
