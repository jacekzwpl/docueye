using DocuEye.Structurizr.Json.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DslToJson
{
    public class WorkspaceSerializer
    {
        private JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            IgnoreReadOnlyFields = true,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { ti =>
                {
                    if (ti.Kind == JsonTypeInfoKind.Object)
                    {
                        for (int i = ti.Properties.Count - 1; i >= 0; i--)
                        {
                            var p = ti.Properties[i];
                            if (p.Set != null) continue; // ma set – zostaw
                            if (p.Get != null) ti.Properties.RemoveAt(i); // tylko get – usuń z serializacji
                        }
                    }
                }}
            }

        };

        public string Serialize(StructurizrJsonWorkspace workspace)
        {
            return JsonSerializer.Serialize(workspace, serializerOptions);
        }

        public StructurizrJsonWorkspace Deserialize(string jsonText)
        {
            return JsonSerializer.Deserialize<StructurizrJsonWorkspace>(jsonText, this.serializerOptions)!;
        }
    }
}
