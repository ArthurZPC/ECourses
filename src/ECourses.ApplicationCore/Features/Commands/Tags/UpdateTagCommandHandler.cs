using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Tags
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagValidator _tagValidator;
        private readonly IEntityValidator<Tag> _entityValidator;
        private readonly ITagConverter _tagConverter;

        public UpdateTagCommandHandler(ITagRepository tagRepository, ITagValidator tagValidator,
            IEntityValidator<Tag> entityValidator,
            ITagConverter tagConverter)
        {
            _tagRepository = tagRepository;
            _tagValidator = tagValidator;
            _entityValidator = entityValidator;
            _tagConverter = tagConverter;
        }

        public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            _tagValidator.ValidateUpdateTagCommand(request);
            await _entityValidator.ValidateIfEntityNotFoundByCondition(t => t.Id == request.Id);
            await _entityValidator.ValidateIfEntityExistsByCondition(t => t.Title.ToLower() == request.Title.ToLower());

            var tag = _tagConverter.ConvertToTag(request);

            await _tagRepository.Update(tag);

            return await Task.FromResult(Unit.Value);
        }
    }
}
