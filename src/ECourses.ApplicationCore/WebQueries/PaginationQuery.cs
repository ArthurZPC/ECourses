using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.WebQueries
{
    public class PaginationQuery
    {
        [Display(Name = "pageNumber")]
        public int PageNumber { get; set; } = 1;

        [Display(Name = "pageSize")]
        public int PageSize { get; set; } = 100;
    }
}
