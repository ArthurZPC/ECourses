using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Tags
{
    public class GetTagQueryHandler : IRequestHandler<GetTagQuery, TagViewModel>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IEntityValidator<Tag> _entityValidator;
        private readonly ITagConverter _tagConverter;

        public GetTagQueryHandler(ITagRepository tagRepository, IEntityValidator<Tag> entityValidator,
            ITagConverter tagConverter)
        {
            _tagRepository = tagRepository;
            _entityValidator = entityValidator;
            _tagConverter = tagConverter;
        }

        public async Task<TagViewModel> Handle(GetTagQuery request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(t => t.Id == request.Id);

            return _tagConverter.ConvertToViewModel(tag!);
        }
    }
}
