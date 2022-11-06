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

namespace ECourses.ApplicationCore.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagConverter _tagConverter;
        private readonly ITagValidator _tagValidator;
        private readonly IEntityValidator<Tag> _entityValidator;

        public TagService(ITagRepository tagRepository, 
            ITagConverter tagConverter, 
            ITagValidator tagValidator, 
            IEntityValidator<Tag> entityValidator)
        {
            _tagRepository = tagRepository;
            _tagConverter = tagConverter;
            _tagValidator = tagValidator;
            _entityValidator = entityValidator;
        }

        public async Task Create(CreateTagViewModel model)
        {
            _tagValidator.ValidateCreateTagViewModel(model);
            await _entityValidator.ValidateIfEntityExistsByCondition(t => t.Title.ToLower() == model.Title.ToLower());

            var tag = _tagConverter.ConvertToTag(model);

            await _tagRepository.Create(tag);
        }

        public async Task Delete(Guid id)
        {
            var tag = await _tagRepository.GetById(id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(t => t.Id == id);

            await _tagRepository.Delete(id);
        }

        public async Task<TagViewModel> GetTagById(Guid id)
        {
            var tag = await _tagRepository.GetById(id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(t => t.Id == id);

            return _tagConverter.ConvertToViewModel(tag!);
        }

        public async Task<PagedListModel<TagViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, TagFilterQuery? filter)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            var filterOptions = MapFilterOptions(filter);
            var orderOptions = MapOrderOptions(orderField);

            var tags = await _tagRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<TagViewModel>
            {
                Count = tags.Count,
                Items = tags.Items.Select(t => _tagConverter.ConvertToViewModel(t))
            };
        }

        public async Task Update(UpdateTagViewModel model)
        {
            _tagValidator.ValidateUpdateTagViewModel(model);
            await _entityValidator.ValidateIfEntityNotFoundByCondition(t => t.Id == model.Id);
            await _entityValidator.ValidateIfEntityExistsByCondition(t => t.Title.ToLower() == model.Title.ToLower());

            var tag = _tagConverter.ConvertToTag(model);

            await _tagRepository.Update(tag);
        }

        private FilterOptions<Tag>? MapFilterOptions(TagFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Tag>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Title is not null)
            {
                predicate = predicate.And(t => t.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            return new FilterOptions<Tag>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Tag>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                TagOrderQueryConstants.TitleAsc => new OrderOptions<Tag>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = t => t.Title
                },
                TagOrderQueryConstants.TitleDesc => new OrderOptions<Tag>
                {
                    Type = OrderType.Descending,
                    FieldSelector = t => t.Title
                },
                _ => null
            };
        }
    }
}
