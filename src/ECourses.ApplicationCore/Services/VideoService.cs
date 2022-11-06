using ECourses.ApplicationCore.Common.Configuration;
using ECourses.ApplicationCore.Common.Constants;
using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Helpers;
using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;
using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ECourses.ApplicationCore.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoConverter _videoConverter;
        private readonly IVideoValidator _videoValidator;
        private readonly IEntityValidator<Video> _videoEntityValidator;
        private readonly IEntityValidator<Course> _courseEntityValidator;
        private readonly WebRootOptions _webRootOptions;

        public VideoService(IVideoRepository videoRepository, 
            IVideoConverter videoConverter, 
            IVideoValidator videoValidator, 
            IEntityValidator<Video> videoEntityValidator, 
            IEntityValidator<Course> courseEntityValidator,
            IOptions<WebRootOptions> webRootOptions)
        {
            _videoRepository = videoRepository;
            _videoConverter = videoConverter;
            _videoValidator = videoValidator;
            _videoEntityValidator = videoEntityValidator;
            _courseEntityValidator = courseEntityValidator;
            _webRootOptions = webRootOptions.Value;
        }

        public async Task Create(CreateVideoViewModel model)
        {
            _videoValidator.ValidateCreateVideoViewModel(model);
            await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == model.CourseId);

            var video = _videoConverter.ConvertToVideo(model);

            var fullPath = GetFullWebRootPath() + video.Url;

            await UploadVideo(model.Video, fullPath);            

           await _videoRepository.Create(video);
        }

        public async Task Delete(Guid id)
        {
            var video = await _videoRepository.GetById(id);

            await _videoEntityValidator.ValidateIfEntityNotFoundByCondition(v => v.Id == id);

            var savedVideoPath = GetFullWebRootPath() + video!.Url;

            RemoveVideo(savedVideoPath);

            await _videoRepository.Delete(id);
        }

        public async Task<PagedListModel<VideoViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, VideoFilterQuery? filter)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            var filterOptions = MapFilterOptions(filter);
            var orderOptions = MapOrderOptions(orderField);

            var videos = await _videoRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<VideoViewModel>
            {
                Count = videos.Count,
                Items = videos.Items.Select(v => _videoConverter.ConvertToViewModel(v))
            };
        }

        public async Task<VideoViewModel> GetVideoById(Guid id)
        {
            var video = await _videoRepository.GetById(id);

            await _videoEntityValidator.ValidateIfEntityNotFoundByCondition(v => v.Id == id);

            return _videoConverter.ConvertToViewModel(video!);
        }

        public async Task Update(UpdateVideoViewModel model)
        {
            _videoValidator.ValidateUpdateVideoViewModel(model);
            await _videoEntityValidator.ValidateIfEntityNotFoundByCondition(v => v.Id == model.Id);

            if (model.CourseId != Guid.Empty)
            {
                await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == model.Id);
            }

            var video = await _videoRepository.GetById(model.Id);

            video!.Title = model.Title != "" ? model.Title : video.Title;
            video.CourseId = model.CourseId != Guid.Empty ? model.CourseId : video.CourseId;

            if (model.Video is not null) { 

                var updatedVideoName = FileNameGenerator.Generate(model.Video);

                var previousFullPath = GetFullWebRootPath() + video!.Url;
                var newFullPath = GetFullWebRootPath() + updatedVideoName;

                RemoveVideo(previousFullPath);

                await UploadVideo(model.Video, newFullPath);

                video.Url = updatedVideoName;
            }

            await _videoRepository.Update(video);
        }

        private string GetFullWebRootPath()
        {
            return Environment.CurrentDirectory + _webRootOptions.WebRootLocation;
        }

        private FilterOptions<Video>? MapFilterOptions(VideoFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Video>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Title is not null)
            {
                predicate = predicate.And(v => v.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            if (filter.CourseId != Guid.Empty)
            {
                predicate = predicate.And(v => v.CourseId == filter.CourseId);
            }

            return new FilterOptions<Video>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Video>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                VideoOrderQueryConstants.TitleAsc => new OrderOptions<Video>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = v => v.Title
                },
                VideoOrderQueryConstants.TitleDesc => new OrderOptions<Video>
                {
                    Type = OrderType.Descending,
                    FieldSelector = v => v.Title
                },
                _ => null
            };
        }

        private void RemoveVideo(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        private async Task UploadVideo(IFormFile file, string fullPath)
        {
            if (file is not null)
            {              
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }
    }
}
