using DocuEye.ChangeTracker.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ChangeTracker.Application.Queries.FindModelChanges
{
    /// <summary>
    /// Finds model changes for workspace
    /// </summary>
    public class FindModelChangesQuery : IRequest<IEnumerable<ModelChange>>
    {
        /// <summary>
        /// Workspace ID
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// Maximum number of items that will be returned
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// Number of items to skip
        /// </summary>
        public int? Skip { get; set; }
    }
}
