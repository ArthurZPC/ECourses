using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Ratings
{
    public class DeleteRatingCommandHandler : IRequestHandler<DeleteRatingCommand>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IEntityValidator<Rating> _entityValidator;

        public DeleteRatingCommandHandler(IRatingRepository ratingRepository, IEntityValidator<Rating> entityValidator)
        {
            _ratingRepository = ratingRepository;
            _entityValidator = entityValidator;
        }

        public async Task<Unit> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(r => r.Id == request.Id);

            await _ratingRepository.Delete(request.Id);

            return await Task.FromResult(Unit.Value);
        }
    }
}
