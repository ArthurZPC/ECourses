using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.WebQueries
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
