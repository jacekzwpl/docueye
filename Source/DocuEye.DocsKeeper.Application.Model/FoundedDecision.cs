using DocuEye.DocsKeeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.DocsKeeper.Application.Model
{
    /// <summary>
    /// Represents decision that was found in query
    /// </summary>
    public class FoundedDecision
    {
        /// <summary>
        /// Decision Id
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// The date that the decision was made (ISO 8601 format).
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// The status of the decision.
        /// </summary>
        public string Status { get; set; } = null!;
        /// <summary>
        /// The title of the decision.
        /// </summary>
        public string Title { get; set; } = null!;
       
        /// <summary>
        /// Decision links
        /// </summary>
        public IEnumerable<DecisionLink> Links { get; set; } = Enumerable.Empty<DecisionLink>();
    }
}
