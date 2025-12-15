using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonConfigurationStyles
    {
        /// <summary>
        /// The set of element styles.
        /// </summary>
        public IEnumerable<StructurizrJsonElementStyle>? Elements { get; set; }
        /// <summary>
        /// The set of relationship styles.
        /// </summary>
        public IEnumerable<StructurizrJsonRelationshipStyle>? Relationships { get; set; }
    }
}
