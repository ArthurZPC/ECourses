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
using ECourses.Data.Identity;

namespace ECourses.ApplicationCore.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorConverter _authorConverter;
        private readonly IAuthorValidator _authorValidator;
        private readonly IEntityValidator<Author> _authorEntityValidator;
        private readonly IEntityValidator<User> _userEntityValidator;

        public AuthorService(IAuthorRepository authorRepository,
            IAuthorConverter authorConverter,
            IAuthorValidator authorValidator,
            IEntityValidator<Author> authorEntityValidator,
            IEntityValidator<User> userEntityValidator)
        {
            _authorRepository = authorRepository;
            _authorConverter = authorConverter;
            _authorValidator = authorValidator;
            _authorEntityValidator = authorEntityValidator;
            _userEntityValidator = userEntityValidator;
        }

        public async Task Create(CreateAuthorViewModel model)
        {
            _authorValidator.ValidateCreateAuthorViewModel(model);
            await _authorEntityValidator.ValidateIfEntityExistsByCondition(a => a.FirstName.ToLower() == model.LastName.ToLower()
            && a.LastName.ToLower() == model.LastName.ToLower() || a.UserId == model.UserId);

            await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == model.UserId);

            var author = _authorConverter.ConvertToAuthor(model);

            await _authorRepository.Create(author);
        }

        public async Task Delete(Guid id)
        {
            var author = await _authorRepository.GetById(id);

            await _authorEntityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == id);

            await _authorRepository.Delete(id);
        }

        public async Task<AuthorViewModel> GetAuthorById(Guid id)
        {
            var author = await _authorRepository.GetById(id);

            await _authorEntityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == id);

            return _authorConverter.ConvertToViewModel(author!);
        }

        public async Task<PagedListModel<AuthorViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, AuthorFilterQuery? filter)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            var filterOptions = MapFilterOptions(filter);
            var orderOptions = MapOrderOptions(orderField);

            var authors = await _authorRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<AuthorViewModel>
            {
                Count = authors.Count,
                Items = authors.Items.Select(a => _authorConverter.ConvertToViewModel(a))
            };
        }

        public async Task Update(UpdateAuthorViewModel model)
        {
            _authorValidator.ValidateUpdateAuthorViewModel(model);
            await _authorEntityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == model.Id);
            await _authorEntityValidator.ValidateIfEntityExistsByCondition(a => a.FirstName.ToLower() == model.LastName.ToLower()
            && a.LastName.ToLower() == model.LastName.ToLower() || a.UserId == model.UserId);

            if (model.UserId != Guid.Empty)
            {
                await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == model.UserId);
            }
           
            var author = await _authorRepository.GetById(model.Id);

            author!.FirstName = model.FirstName != "" ? model.FirstName : author.FirstName;
            author.LastName = model.LastName != "" ? model.LastName : author.LastName;
            author.UserId = model.UserId != Guid.Empty ? model.UserId : author.UserId;

            await _authorRepository.Update(author);
        }

        private FilterOptions<Author>? MapFilterOptions(AuthorFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Author>();

            if (filter is null)
            {
                return null;
            }

            if (filter.FirstName is not null)
            {
                predicate = predicate.And(a => a.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (filter.LastName is not null)
            {
                predicate = predicate.And(a => a.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (filter.UserId != Guid.Empty)
            {
                predicate = predicate.And(a => a.UserId == filter.UserId);
            }

            return new FilterOptions<Author>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Author>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                AuthorOrderQueryConstants.FirstNameAsc => new OrderOptions<Author>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = a => a.FirstName
                },
                AuthorOrderQueryConstants.FirstNameDesc => new OrderOptions<Author>
                {
                    Type = OrderType.Descending,
                    FieldSelector = a => a.FirstName
                },

                AuthorOrderQueryConstants.LastNameAsc => new OrderOptions<Author>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = a => a.LastName
                },
                AuthorOrderQueryConstants.LastNameDesc => new OrderOptions<Author>
                {
                    Type = OrderType.Descending,
                    FieldSelector = a => a.LastName
                },
                _ => null
            };
        }
    }
}
