namespace ECourses.ApplicationCore.ViewModels
{
    public class RatingViewModel
    {
        public Guid Id { get; set; }
        public int Value { get; set; }

        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
    }
}
