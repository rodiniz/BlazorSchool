namespace BlazorSchoolShared.Dto
{
    public record StudentDto
    {
        public string Id { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
    }
}
