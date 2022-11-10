using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Categories
{
    public class GetCategoryQuery : IRequest<CategoryViewModel>
    {
        public Guid Id { get; set; }
    }
}
