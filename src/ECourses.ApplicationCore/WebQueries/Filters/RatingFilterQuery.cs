namespace ECourses.ApplicationCore.WebQueries.Filters
{
    public class RatingFilterQuery
    {
        public int? Value { get; set; }

        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
    }
}
