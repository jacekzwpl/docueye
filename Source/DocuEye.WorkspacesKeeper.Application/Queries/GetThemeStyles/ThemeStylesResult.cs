using DocuEye.WorkspacesKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspacesKeeper.Application.Queries.GetThemeStyles
{
    /// <summary>
    /// Result of getting theme styles from url
    /// </summary>
    public class ThemeStylesResult
    {
        /// <summary>
        /// Element styles 
        /// </summary>
        public IEnumerable<ElementStyle>? Elements { get; set; }
        /// <summary>
        /// Relationship styles
        /// </summary>
        public IEnumerable<RelationshipStyle>? Relationships { get; set; }
    }
}
