using BlazorSchoolApi.Interfaces;

namespace BlazorSchoolApi.Data
{
    public class StudentTests : IEntity
    {
        public int Id { get; set; }

        public CourseCycle CourseCycle { get; set; }

    }
}
