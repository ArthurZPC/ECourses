﻿using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using ECourses.Data.Identity;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Authors
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorValidator _authorValidator;
        private readonly IEntityValidator<Author> _authorEntityValidator;
        private readonly IAuthorConverter _authorConverter;
        private readonly IEntityValidator<User> _userEntityValidator;
        private readonly IRabbitMQService _rabbitMQService;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IAuthorValidator authorValidator, 
            IEntityValidator<Author> authorEntityValidator, 
            IAuthorConverter authorConverter, 
            IEntityValidator<User> userEntityValidator,
            IRabbitMQService rabbitMQService)
        {
            _authorRepository = authorRepository;
            _authorValidator = authorValidator;
            _authorEntityValidator = authorEntityValidator;
            _authorConverter = authorConverter;
            _userEntityValidator = userEntityValidator;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            _authorValidator.ValidateCreateAuthorCommand(request);
            await _authorEntityValidator.ValidateIfEntityExistsByCondition(a => a.FirstName.ToLower() == request.LastName.ToLower()
            && a.LastName.ToLower() == request.LastName.ToLower() || a.UserId == request.UserId);

            await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == request.UserId);

            var author = _authorConverter.ConvertToAuthor(request);

            await _authorRepository.Create(author);

            var loggingMessage = new CommandLoggingMessage<CreateAuthorCommand>(request, CommandType.Create, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
