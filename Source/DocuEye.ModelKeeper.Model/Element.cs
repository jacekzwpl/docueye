using DocuEye.Infrastructure.Persistence.Model;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Model
{
    /// <summary>
    /// Model element
    /// </summary>
    public class Element : BaseEntity
    {
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
        /// Id of parent element for this element
        /// </summary>
        public string? ParentId { get; set; }
        /// <summary>
        /// Id of element that this element is instance of
        /// </summary>
        public string? InstanceOfId { get; set; }
        /// <summary>
        /// The workspace id of this element 
        /// </summary>
        public string WorkspaceId {  get; set; } = null!;
        /// <summary>
        /// A set of arbitrary name-value properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// The source code of the element
        /// </summary>
        public string? SourceCodeUrl { get; set; }
        /// <summary>
        /// The name of owner team of this element
        /// </summary>
        public string? OwnerTeam { get; set; }
    }
}
