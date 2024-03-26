using System.ComponentModel;

namespace BlazorSchoolShared
{
    public enum SortDirection
    {
        [Description("none")] None,
        [Description("ascending")] Ascending,
        [Description("descending")] Descending,
    }
    public class TableStateDto
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string? SortLabel { get; set; }

        public SortDirection SortDirection { get; set; }
    }
}
