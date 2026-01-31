using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonCustomElementMap
    {
        public static LinterModelElement ToLinterModelElement(this StructurizrJsonCustomElement source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new LinterModelElement()
            {
                Identifier = source.DslId,
                Name = source.Name ?? string.Empty,
                Tags = string.IsNullOrWhiteSpace(source.Tags) ? new List<string>() : source.Tags.Split(",").ToList(),
                Description = source.Description,
                Properties = source.Properties != null ? new Dictionary<string, string>(source.Properties) : new Dictionary<string, string>(),
                JsonModelId = source.Id
            };
        }
    }
}
