using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface ITagConverter
    {
        Tag ConvertToTag(TagViewModel model);
        Tag ConvertToTag(CreateTagViewModel model);
        Tag ConvertToTag(UpdateTagViewModel model);
        TagViewModel ConvertToViewModel(Tag model);
    }
}
