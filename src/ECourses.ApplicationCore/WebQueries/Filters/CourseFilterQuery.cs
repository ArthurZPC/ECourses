namespace ECourses.ApplicationCore.WebQueries.Filters
{
    public class CourseFilterQuery
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedAt { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }

        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }

    }
}
