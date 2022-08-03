using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.WebQueries.Filters
{
    public class CategoryFilterQuery
    {
        [Display(Name = "title")]
        public string? Title { get; set; }
    }
}
