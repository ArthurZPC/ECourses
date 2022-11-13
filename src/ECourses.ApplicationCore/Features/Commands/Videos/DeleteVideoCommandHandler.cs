using ECourses.ApplicationCore.Common.Configuration;
using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace ECourses.ApplicationCore.Features.Commands.Videos
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IEntityValidator<Video> _entityValidator;
        private readonly IFileService _fileService;
        private readonly WebRootOptions _webRootOptions;

        public DeleteVideoCommandHandler(IVideoRepository videoRepository, IEntityValidator<Video> entityValidator,
            IFileService fileService,
            IOptions<WebRootOptions> webRootOptions)
        {
            _videoRepository = videoRepository;
            _entityValidator = entityValidator;
            _fileService = fileService;
            _webRootOptions = webRootOptions.Value;
        }

        public async Task<Unit> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var video = await _videoRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(v => v.Id == request.Id);

            var savedVideoPath = GetFullWebRootPath() + video!.Url;

            _fileService.Delete(savedVideoPath);
            await _videoRepository.Delete(request.Id);

            return await Task.FromResult(Unit.Value);
        }

        private string GetFullWebRootPath()
        {
            return Environment.CurrentDirectory + _webRootOptions.WebRootLocation;
        }
    }
}
