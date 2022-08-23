using AllDto.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreAPI.Code.Filters
{
    /// <summary>
    /// "multipart/form-data" 表单提交模型，注释、默认值、隐藏属性处理
    /// </summary>
    public class IgnorePropertyRequestBodyFilter : IRequestBodyFilter
    {
        public void Apply(OpenApiRequestBody requestBody, RequestBodyFilterContext context)
        {
            if (requestBody.Content.Keys.Contains("multipart/form-data"))
            {
                var pro = typeof(SchemaRepository).GetField("_reservedIds", BindingFlags.NonPublic | BindingFlags.Instance);
                if (pro == null)
                    return;

                //var schemaTypes = (Dictionary<Type, string>)pro.GetValue(context.SchemaRepository);
                var pros = requestBody.Content["multipart/form-data"].Schema.Properties;

                foreach (var schema in pros)
                {
                    var s = context.FormParameterDescriptions.FirstOrDefault(p => p.Name == schema.Key);
                    var displayAttr = s?.ModelMetadata.DisplayName;
                    var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(s.PropertyInfo(), typeof(DescriptionAttribute));
                    var defaultValue = (DefaultValueAttribute)Attribute.GetCustomAttribute(s.PropertyInfo(), typeof(DefaultValueAttribute));
                    var ignoreProperties = (IgnorePropertyAttribute)Attribute.GetCustomAttribute(s.PropertyInfo(), typeof(IgnorePropertyAttribute));
                    
                    if (ignoreProperties != null)
                    {
                        pros.Remove(schema.Key);
                        continue;
                    }

                    if(defaultValue != null)
                    {
                        schema.Value.Default = new Microsoft.OpenApi.Any.OpenApiString(defaultValue.Value?.ToString());
                    }

                    if (displayAttr != null)
                    {
                        if (schema.Value.Reference != null)
                        {
                            var values = context.SchemaRepository.Schemas[schema.Value.Reference.Id];
                            schema.Value.Enum = values.Enum;
                            schema.Value.Type = values.Type;
                            schema.Value.Reference = null;
                        }
                        schema.Value.Description = displayAttr;
                        continue;
                    }
                    if (descAttr != null && descAttr.Description.NotNull())
                    {
                        schema.Value.Description = descAttr.Description;
                    }
                }
            }
            
        }
    }
}
