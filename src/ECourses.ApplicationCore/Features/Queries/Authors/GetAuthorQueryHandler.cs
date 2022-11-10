using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Authors
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, AuthorViewModel>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorConverter _authorConverter;
        private readonly IEntityValidator<Author> _authorEntityValidator;

        public GetAuthorQueryHandler(IAuthorRepository authorRepository, IAuthorConverter authorConverter,
            IEntityValidator<Author> authorEntityValidator)
        {
            _authorRepository = authorRepository;
            _authorConverter = authorConverter;
            _authorEntityValidator = authorEntityValidator;
        }

        public async Task<AuthorViewModel> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetById(request.Id);

            await _authorEntityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == request.Id);

            return _authorConverter.ConvertToViewModel(author!);
        }
    }
}
