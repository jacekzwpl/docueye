using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Views
{
    public class ElementInViewToImport
    {
        /// <summary>
        /// The ID of element in structurizr json
        /// </summary>
        public string StructurizrId { get; set; } = null!;
        /// <summary>
        /// The horizontal position of the element when rendered.
        /// </summary>
        public int? X { get; set; }
        /// <summary>
        /// The vertical position of the element when rendered.
        /// </summary>
        public int? Y { get; set; }
    }
}
