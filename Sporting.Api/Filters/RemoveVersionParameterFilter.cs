using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Sporting.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveVersionParameterFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="operationFilterContext"></param>
        public void Apply(Operation operation, OperationFilterContext operationFilterContext)
        {
            var parameterVersion = operation?.Parameters?.SingleOrDefault(param => param.Name.Equals("version"));
            operation?.Parameters?.Remove(parameterVersion);
        }
    }
}
