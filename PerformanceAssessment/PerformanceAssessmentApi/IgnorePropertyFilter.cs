using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

namespace PerformanceAssessmentApi
{
    public class IgnorePropertyFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription == null || operation.Parameters == null)
                return;

            foreach (var parameterDescription in context.ApiDescription.ParameterDescriptions)
            {
                if (parameterDescription.Source.Equals(BindingSource.Form)
                    && parameterDescription.CustomAttributes().Any(attr => attr.GetType().Equals(typeof(JsonIgnoreAttribute))))
                {
                    operation.RequestBody.Content.Values.Single(v => v.Schema.Properties.Remove(parameterDescription.Name));
                }
            }

            foreach (var parameterDescription in context.ApiDescription.ParameterDescriptions)
            {
                if (parameterDescription.Source.Equals(BindingSource.Query)
                    && parameterDescription.CustomAttributes().Any(attr => attr.GetType().Equals(typeof(JsonIgnoreAttribute))))
                {
                    operation.Parameters.Remove(operation.Parameters.Single(w => w.Name.Equals(parameterDescription.Name)));
                }
            }

            var excludedProperties = context.ApiDescription.ParameterDescriptions.Where(p =>
                p.Source.Equals(BindingSource.Form));

            if (excludedProperties.Any())
            {
                foreach (var excludedProperty in excludedProperties)
                {
                    foreach (var customAttribute in excludedProperty.CustomAttributes())
                    {
                        if (customAttribute.GetType() == typeof(JsonIgnoreAttribute))
                        {
                            for (int i = 0; i < operation.RequestBody.Content.Values.Count; i++)
                            {
                                for (int j = 0; j < operation.RequestBody.Content.Values.ElementAt(i).Encoding.Count; j++)
                                {
                                    if (operation.RequestBody.Content.Values.ElementAt(i).Encoding.ElementAt(j).Key ==
                                        excludedProperty.Name)
                                    {
                                        operation.RequestBody.Content.Values.ElementAt(i).Encoding
                                            .Remove(operation.RequestBody.Content.Values.ElementAt(i).Encoding.ElementAt(j));
                                        operation.RequestBody.Content.Values.ElementAt(i).Schema.Properties.Remove(excludedProperty.Name);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}