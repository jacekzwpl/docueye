using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrConfiguration
    {
        public StructurizrConfigurationStyles? Styles { get; set; }
        public IEnumerable<string>? Themes { get; set; }
        public StructurizrTerminology? Terminology { get; set; }
    }
}
