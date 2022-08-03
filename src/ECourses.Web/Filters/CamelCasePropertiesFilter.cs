using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ECourses.Web.Filters
{
    public class CamelCasePropertiesFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters is null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            foreach(var parameter in operation.Parameters)
            {
                parameter.Name = parameter.Name.ToLower()[0] + parameter.Name.Substring(1);
            }
        }
    }
}
