using BlazorSchoolApi.Interfaces;

namespace BlazorSchoolApi.Data
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
    }
}
