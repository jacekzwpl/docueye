using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model.Elements
{
    public class ElementToImport
    {
        /// <summary>
        /// The ID of the element in structurizr json
        /// </summary>
        public string StructurizrId { get; set; } = null!;
        /// <summary>
        /// The ID of parent element in structurizr json
        /// </summary>
        public string? StructurizrParentId { get; set; }
        /// <summary>
        /// The ID of element in structurizr json that this element is instance of
        /// </summary>
        public string? StructurizrInstanceOfId { get; set; }
        /// <summary>
        /// Structurizr DSL Identifier
        /// </summary>
        public string DslId { get; set; } = null!;
        /// <summary>
        /// The name of this element
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// The element detail url
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// The groups of the element
        /// </summary>
        public string? Group { get; set; }
        /// <summary>
        /// The tags of the element
        /// </summary>
        public IEnumerable<string>? Tags { get; set; }
        /// <summary>
        /// The type of the element
        /// </summary>
        public string Type { get; set; } = null!;
        /// <summary>
        /// The location of the element
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// The technology of the element
        /// </summary>
        public string? Technology { get; set; }
        /// <summary>
        /// The description of the element
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// A set of arbitrary name-value properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

    }
}
