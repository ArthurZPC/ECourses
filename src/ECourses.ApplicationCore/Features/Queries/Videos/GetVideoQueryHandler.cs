using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Videos
{
    public class GetVideoQueryHandler : IRequestHandler<GetVideoQuery, VideoViewModel>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IEntityValidator<Video> _entityValidator;
        private readonly IVideoConverter _videoConverter;

        public GetVideoQueryHandler(IVideoRepository videoRepository, IEntityValidator<Video> entityValidator,
            IVideoConverter videoConverter)
        {
            _videoRepository = videoRepository;
            _entityValidator = entityValidator;
            _videoConverter = videoConverter;
        }
        public async Task<VideoViewModel> Handle(GetVideoQuery request, CancellationToken cancellationToken)
        {
            var video = await _videoRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(v => v.Id == request.Id);

            return _videoConverter.ConvertToViewModel(video!);
        }
    }
}
