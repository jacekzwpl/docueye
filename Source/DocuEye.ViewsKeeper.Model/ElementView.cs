using DocuEye.Infrastructure.Persistence.Model;
using System.Collections.Generic;

namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// Element in view
    /// </summary>
    public class ElementView : BaseEntity
    {
        
        /// <summary>
        /// The horizontal position of the element when rendered.
        /// </summary>
        public int? X { get; set; }
        /// <summary>
        /// The vertical position of the element when rendered.
        /// </summary>
        public int? Y { get; set; }

        /// <summary>
        /// The ID of the context diagram for this element
        /// </summary>
        public string? DiagramId { get; set; }

        /// <summary>
        /// Structurizr DSL Identifier
        /// </summary>
        public string DslId { get; set; } = null!;
        /// <summary>
        /// The name of this element
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Group { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string>? Tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Technology { get; set; }
        /// <summary>
        /// 
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
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// A set of arbitrary name-value properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public string? SourceCodeUrl { get; set; }
        public string? OwnerTeam { get; set; }

    }
}
