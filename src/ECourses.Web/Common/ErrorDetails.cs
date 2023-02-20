using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ECourses.Web.Common
{
    [Serializable]
    public record ErrorDetails
    {
        [Required]
        public string ErrorMessage { get; init; } = string.Empty;

        public override string ToString()
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(this, serializerOptions);
        }
    }
}
