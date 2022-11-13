﻿using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Categories
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryValidator _categoryValidator;
        private readonly IEntityValidator<Category> _entityValidator;
        private readonly ICategoryConverter _categoryConverter;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryValidator categoryValidator, 
            IEntityValidator<Category> entityValidator, 
            ICategoryConverter categoryConverter)
        {
            _categoryRepository = categoryRepository;
            _categoryValidator = categoryValidator;
            _entityValidator = entityValidator;
            _categoryConverter = categoryConverter;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            _categoryValidator.ValidateUpdateCategoryCommand(request);
            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.Id);
            await _entityValidator.ValidateIfEntityExistsByCondition(c => c.Title.ToLower() == request.Title.ToLower());

            var category = _categoryConverter.ConvertToCategory(request);

            await _categoryRepository.Update(category);

            return await Task.FromResult(Unit.Value);
        }
    }
}