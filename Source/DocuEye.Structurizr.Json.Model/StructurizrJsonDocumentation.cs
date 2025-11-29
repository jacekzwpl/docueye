using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonDocumentation
    {
        public IEnumerable<StructurizrJsonDocumentationSection>? Sections { get; set; }
        public IEnumerable<StructurizrJsonDecision>? Decisions { get; set; }
        public IEnumerable<StructurizrJsonImage>? Images { get; set; }

    }
}
