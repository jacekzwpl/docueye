using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrConfigurationStyles
    {
        /// <summary>
        /// The set of element styles.
        /// </summary>
        public IEnumerable<StructurizrElementStyle>? Elements { get; set; }
        /// <summary>
        /// The set of relationship styles.
        /// </summary>
        public IEnumerable<StructurizrRelationshipStyle>? Relationships { get; set; }
    }
}
