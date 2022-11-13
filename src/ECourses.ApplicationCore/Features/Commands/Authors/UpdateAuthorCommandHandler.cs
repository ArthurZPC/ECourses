﻿using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using ECourses.Data.Identity;
using ECourses.Data.Repositories;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Authors
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorValidator _authorValidator;
        private readonly IEntityValidator<Author> _authorEntityValidator;
        private readonly IEntityValidator<User> _userEntityValidator;
        private readonly IAuthorConverter _authorConverter;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IAuthorValidator authorValidator, 
            IEntityValidator<Author> authorEntityValidator, 
            IEntityValidator<User> userEntityValidator,
            IAuthorConverter authorConverter)
        {
            _authorRepository = authorRepository;
            _authorValidator = authorValidator;
            _authorEntityValidator = authorEntityValidator;
            _userEntityValidator = userEntityValidator;
            _authorConverter = authorConverter;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            _authorValidator.ValidateUpdateAuthorCommand(request);
            await _authorEntityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == request.Id);
            await _authorEntityValidator.ValidateIfEntityExistsByCondition(a => a.FirstName.ToLower() == request.FirstName.ToLower()
            && a.LastName.ToLower() == request.LastName.ToLower() || a.UserId == request.UserId);

            if (request.UserId != Guid.Empty)
            {
                await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == request.UserId);
            }

            var author = _authorConverter.ConvertToAuthor(request);
            await _authorRepository.Update(author);

            return await Task.FromResult(Unit.Value);
        }
    }
}
