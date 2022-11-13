using ECourses.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ECourses.Data.Entities
{
    public class Course : Entity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? PublishedAt { get; set; }

        [Precision(12,2)]
        public decimal? Price { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = default!;

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        public ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}
