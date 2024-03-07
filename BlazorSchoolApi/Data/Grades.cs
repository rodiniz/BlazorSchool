namespace BlazorSchoolApi.Data
{
    public class Grades
    {
        public int Id { get; set; }
        public virtual StudentTests StudentTests { get; set; }
        public virtual Student Student { get; set; }
        public string Score { get; set; }  
    }
}
