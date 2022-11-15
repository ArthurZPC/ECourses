using ECourses.ApplicationCore.Common.Configuration;
using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace ECourses.ApplicationCore.Features.Commands.Videos
{
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoValidator _videoValidator;
        private readonly IEntityValidator<Course> _courseEntityValidator;
        private readonly IVideoConverter _videoConverter;
        private readonly IFileService _fileService;
        private readonly WebRootOptions _webRootOptions;
        private readonly IRabbitMQService _rabbitMQService;

        public CreateVideoCommandHandler(IVideoRepository videoRepository, IVideoValidator videoValidator, 
            IEntityValidator<Course> courseEntityValidator, 
            IVideoConverter videoConverter, 
            IFileService fileService,
            IOptions<WebRootOptions> webRootOptions,
            IRabbitMQService rabbitMQService)
        {
            _videoRepository = videoRepository;
            _videoValidator = videoValidator;
            _courseEntityValidator = courseEntityValidator;
            _videoConverter = videoConverter;
            _fileService = fileService;
            _webRootOptions = webRootOptions.Value;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            _videoValidator.ValidateCreateVideoCommand(request);
            await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.CourseId);

            var video = _videoConverter.ConvertToVideo(request);

            var fullPath = GetFullWebRootPath() + video.Url;

            await _fileService.UploadFormFileAsync(request.Video, fullPath);
            await _videoRepository.Create(video);

            var loggingMessage = new CommandLoggingMessage<CreateVideoCommand>(request, CommandType.Create, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }

        private string GetFullWebRootPath()
        {
            return Environment.CurrentDirectory + _webRootOptions.WebRootLocation;
        }
    }
}
