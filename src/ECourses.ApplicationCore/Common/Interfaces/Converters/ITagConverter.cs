using ECourses.ApplicationCore.Features.Commands.Tags;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface ITagConverter
    {
        Tag ConvertToTag(TagViewModel model);
        Tag ConvertToTag(CreateTagCommand command);
        Tag ConvertToTag(UpdateTagCommand command);
        TagViewModel ConvertToViewModel(Tag model);
    }
}
