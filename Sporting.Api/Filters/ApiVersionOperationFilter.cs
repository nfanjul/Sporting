using Sporting.Api.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Sporting.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiVersionOperationFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="operationFilterContext"></param>
        public void Apply(Operation operation, OperationFilterContext operationFilterContext)
        {
            var actionApiVersionModel = operationFilterContext?.ApiDescription?.ActionDescriptor?.GetApiVersion();
            if (actionApiVersionModel == null)
            {
                return;
            }

            if (actionApiVersionModel.DeclaredApiVersions.Any())
            {
                operation.Produces = operation.Produces
                  .SelectMany(seclaredApiVersions => actionApiVersionModel.DeclaredApiVersions
                    .Select(version => $"{seclaredApiVersions};v={version.ToString()}")).ToList();
            }
            else
            {
                operation.Produces = operation.Produces
                  .SelectMany(implementedApiVersions => actionApiVersionModel.ImplementedApiVersions.OrderByDescending(o => o)
                    .Select(version => $"{implementedApiVersions};v={version.ToString()}")).ToList();
            }
        }
    }
}
