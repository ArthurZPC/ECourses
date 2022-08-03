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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryConverter _categoryConverter;
        private readonly ICategoryValidator _categoryValidator;
        private readonly IEntityValidator<Category> _entityValidator;

        public CategoryService(ICategoryRepository categoryRepository,
            ICategoryConverter categoryConverter,
            ICategoryValidator categoryValidator,
            IEntityValidator<Category> entityValidator)
        {
            _categoryRepository = categoryRepository;
            _categoryConverter = categoryConverter;
            _categoryValidator = categoryValidator;
            _entityValidator = entityValidator;
        }

        public async Task Create(CreateCategoryViewModel model)
        {
            _categoryValidator.ValidateCreateCategoryViewModel(model);
            await _entityValidator.ValidateIfEntityExistsByCondition(c => c.Title.ToLower() == model.Title.ToLower());

            var category = _categoryConverter.ConvertToCategory(model);

            await _categoryRepository.Create(category);
        }

        public async Task Delete(Guid id)
        {
            var category = await _categoryRepository.GetById(id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == id);

            await _categoryRepository.Delete(id);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAll();

            return categories.Select(c => _categoryConverter.ConvertToViewModel(c)).ToList();
        }

        public async Task<CategoryViewModel> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetById(id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == id);

            return _categoryConverter.ConvertToViewModel(category!);
        }

        public async Task<PagedListModel<CategoryViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, CategoryFilterQuery? filter)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            var filterOptions = MapFilterOptions(filter);
            var orderOptions = MapOrderOptions(orderField);

            var categories = await _categoryRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<CategoryViewModel>
            {
                Count = categories.Count,
                Items = categories.Items.Select(c => _categoryConverter.ConvertToViewModel(c))
            };
        }
        public async Task Update(UpdateCategoryViewModel model)
        {
            _categoryValidator.ValidateUpdateCategoryViewModel(model);
            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == model.Id);
            await _entityValidator.ValidateIfEntityExistsByCondition(c => c.Title.ToLower() == model.Title.ToLower());      

            var category = _categoryConverter.ConvertToCategory(model);

            await _categoryRepository.Update(category);
        }

        private FilterOptions<Category>? MapFilterOptions(CategoryFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Category>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Title is not null)
            {
                predicate = predicate.And((c) => c.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            return new FilterOptions<Category>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Category>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                CategoryOrderQueryConstants.TitleAsc => new OrderOptions<Category>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = c => c.Title
                },
                CategoryOrderQueryConstants.TitleDesc => new OrderOptions<Category>
                {
                    Type = OrderType.Descending,
                    FieldSelector = c => c.Title
                },
                _ => null,
            };
        }
    }
}
