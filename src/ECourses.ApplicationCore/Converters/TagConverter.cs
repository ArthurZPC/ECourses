using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Converters
{
    public class TagConverter : ITagConverter
    {
        public Tag ConvertToTag(TagViewModel model)
        {
            return new Tag()
            {
                Id = model.Id,
                Title = model.Title,
            };
        }

        public Tag ConvertToTag(CreateTagViewModel model)
        {
            return new Tag()
            {
                Title = model.Title,
            };
        }

        public Tag ConvertToTag(UpdateTagViewModel model)
        {
            return new Tag()
            {
                Id = model.Id,
                Title = model.Title,
            };
        }

        public TagViewModel ConvertToViewModel(Tag model)
        {
            return new TagViewModel()
            {
                Id = model.Id,
                Title = model.Title,
            };
        }
    }
}
