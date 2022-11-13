using ECourses.ApplicationCore.Common.Configuration;
using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Helpers;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace ECourses.ApplicationCore.Features.Commands.Videos
{
    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoValidator _videoValidator;
        private readonly IEntityValidator<Video> _videoEntityValidator;
        private readonly IEntityValidator<Course> _courseEntityValidator;
        private readonly IFileService _fileService;
        private readonly IVideoConverter _videoConverter;
        private readonly WebRootOptions _webRootOptions;

        public UpdateVideoCommandHandler(IVideoRepository videoRepository, 
            IVideoValidator videoValidator, 
            IEntityValidator<Video> videoEntityValidator, 
            IEntityValidator<Course> courseEntityValidator,
            IFileService fileService,
            IVideoConverter videoConverter,
            IOptions<WebRootOptions> webRootOptions)
        {
            _videoRepository = videoRepository;
            _videoValidator = videoValidator;
            _videoEntityValidator = videoEntityValidator;
            _courseEntityValidator = courseEntityValidator;
            _fileService = fileService;
            _videoConverter = videoConverter;
            _webRootOptions = webRootOptions.Value;
        }

        public async Task<Unit> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            _videoValidator.ValidateUpdateVideoCommand(request);
            await _videoEntityValidator.ValidateIfEntityNotFoundByCondition(v => v.Id == request.Id);

            if (request.CourseId != Guid.Empty)
            {
                await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.Id);
            }

            var currentVideo = await _videoRepository.GetById(request.Id);

            var newFileName = request.NewUrl ?? FileNameGenerator.Generate(request.Video);

            var videoExtension = _fileService.ContainsExtension(newFileName) ?
                _fileService.GetExtension(newFileName) : 
                _fileService.GetExtension(currentVideo!.Url);

            if (!_fileService.ContainsExtension(newFileName))
            {
                newFileName += videoExtension;
            }

            var previousFullPath = GetFullWebRootPath() + currentVideo!.Url;
            var newFullPath = GetFullWebRootPath() + newFileName;

            if (request.Video is not null)
            {
                _fileService.Delete(previousFullPath);
                await _fileService.UploadFormFileAsync(request.Video, newFullPath);

                request.NewUrl = newFileName;
            }
            else if (request.NewUrl is not null)
            {
                _fileService.Move(previousFullPath, newFullPath);

                request.NewUrl = newFileName;
            }

            var newVideo = _videoConverter.ConvertToVideo(request);

            await _videoRepository.Update(newVideo);

            return await Task.FromResult(Unit.Value);
        }

        private string GetFullWebRootPath()
        {
            return Environment.CurrentDirectory + _webRootOptions.WebRootLocation;
        }
    }
}
