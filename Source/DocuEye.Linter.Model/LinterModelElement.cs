using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Linter.Model
{
    public class LinterModelElement
    {
        /// <summary>
        /// Model element identifier
        /// </summary>
        public string Identifier { get; set; } = null!;
        /// <summary>
        /// Identifier of the parent model element, if any
        /// </summary>
        public string? ParentIdentifier { get; set; } = null;
        /// <summary>
        /// Model element name
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Model element tags
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();
        /// <summary>
        /// Technology associated with the model element, if any
        /// </summary>
        public string? Technology { get; set; } = null;
        /// <summary>
        /// Model element description
        /// </summary>
        public string? Description { get; set; } = null;
        /// <summary>
        /// Model element properties
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Identifier of the model element this instance is based on, if any
        /// </summary>
        public string? InstanceOfIdentifier { get; set; } = null;
        /// <summary>
        /// Identifier of the JSON model element this instance is based on, if any
        /// </summary>
        public string? JsonModelId { get; set; }

    }
}
