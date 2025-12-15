using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonConfiguration
    {
        public StructurizrJsonConfigurationStyles? Styles { get; set; }
        public IEnumerable<string>? Themes { get; set; }
        public StructurizrJsonTerminology? Terminology { get; set; }
    }
}
