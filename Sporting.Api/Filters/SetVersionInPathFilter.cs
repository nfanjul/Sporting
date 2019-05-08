using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Sporting.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class SetVersionInPathFilter : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="documentFilterContext"></param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext documentFilterContext)
        {
                swaggerDoc.Paths = swaggerDoc.Paths
                .ToDictionary(
                    path => path.Key + $"?api-version={swaggerDoc.Info.Version}",
                    path => path.Value
                );
        }
    }
}
