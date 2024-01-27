using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrDocumentation
    {
        public IEnumerable<StructurizrDocumentationSection>? Sections { get; set; }
        public IEnumerable<StructurizrDecision>? Decisions { get; set; }
        public IEnumerable<StructurizrImage>? Images { get; set; }

    }
}
