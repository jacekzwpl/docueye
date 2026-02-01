using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Linter.Model
{
    public class LinterModelRelationship
    {

        public string Identifier { get; set; } = null!;
        /// <summary>
        /// Relationship source element.
        /// </summary>
        public LinterModelElement Source { get; set; } = null!;
        /// <summary>
        /// Relationship destination element.
        /// </summary>
        public LinterModelElement Destination { get; set; } = null!;
        /// <summary>
        /// Technology associated with the relationship, if any
        /// </summary>
        public string? Technology { get; set; } = null;
        /// <summary>
        /// Description of the relationship
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Properties of the relationship
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Tags associated with the relationship
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();
        public string? JsonModelId { get; set; }

    }
}
