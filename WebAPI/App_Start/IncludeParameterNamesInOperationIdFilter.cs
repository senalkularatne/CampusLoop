using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace UCEvents_WebAPI.App_Start
{
    /// <summary>
    /// This is for the swagger documentation
    /// </summary>
    internal class IncludeParameterNamesInOperationIdFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters != null)
            {
                // Select the capitalized parameter names
                var parameters = operation.parameters.Select(
                    p => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(p.name));

                // Set the operation id to match the format "OperationByParam1AndParam2"
                operation.operationId = string.Format(
                    "{0}By{1}",
                    operation.operationId,
                    string.Join("And", parameters));
            }
        }
    }
}