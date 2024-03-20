using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Services
{
    public class StudentService : ICrudService<StudentDto,int>
    {
        
        private readonly SchoolContext _context;
        
        private readonly IUserService _userService;
        public StudentService(
            SchoolContext context,
            IUserService userService)
        {
        
            _context = context;
            _userService = userService;
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
            var id= await _userService.CreateUser(model.Email,model.Name,"Student");
            if (string.IsNullOrEmpty(id))
            {
                return TypedResults.BadRequest();
            }
            var student = new Student
            {
                Name = model.Name,
                Address = model.Address,
                BirthDate = model.BirthDate ?? DateTime.Now,
                UserId = id
            };
            await _context.Students.AddAsync(student);
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

       
    }
}
