using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Tags
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IEntityValidator<Tag> _entityValidator;

        public DeleteTagCommandHandler(ITagRepository tagRepository, IEntityValidator<Tag> entityValidator)
        {
            _tagRepository = tagRepository;
            _entityValidator = entityValidator;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(t => t.Id == request.Id);

            await _tagRepository.Delete(request.Id);

            return await Task.FromResult(Unit.Value);
        }
    }
}
