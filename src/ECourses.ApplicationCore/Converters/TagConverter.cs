using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Features.Commands.Tags;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Converters
{
    public class TagConverter : ITagConverter
    {
        public Tag ConvertToTag(TagViewModel model)
        {
            return new Tag
            {
                Id = model.Id,
                Title = model.Title,
            };
        }

        public Tag ConvertToTag(CreateTagCommand command)
        {
            return new Tag
            {
                Title = command.Title,
            };
        }

        public Tag ConvertToTag(UpdateTagCommand command)
        {
            return new Tag
            {
                Id = command.Id,
                Title = command.Title,
            };
        }

        public TagViewModel ConvertToViewModel(Tag model)
        {
            return new TagViewModel
            {
                Id = model.Id,
                Title = model.Title,
            };
        }
    }
}
