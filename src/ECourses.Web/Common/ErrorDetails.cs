using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ECourses.Web.Common
{
    [Serializable]
    public class ErrorDetails
    {
        [Required]
        public string ErrorMessage { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
