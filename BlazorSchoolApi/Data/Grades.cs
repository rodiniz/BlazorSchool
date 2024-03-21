using BlazorSchoolApi.Interfaces;

namespace BlazorSchoolApi.Data
{
    public class Grades : IEntity
    {
        public int Id { get; set; }
        public virtual StudentTests StudentTests { get; set; }
        public virtual ApplicationUser Student { get; set; }
        public string Score { get; set; }  
    }
}
