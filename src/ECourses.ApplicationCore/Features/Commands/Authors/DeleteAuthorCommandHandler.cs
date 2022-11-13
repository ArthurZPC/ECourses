using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Authors
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IEntityValidator<Author> _entityValidator;
        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IEntityValidator<Author> entityValidator)
        {
            _authorRepository = authorRepository;
            _entityValidator = entityValidator;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == request.Id);

            await _authorRepository.Delete(request.Id);

            return await Task.FromResult(Unit.Value);
        }
    }
}
