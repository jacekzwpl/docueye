﻿using DocuEye.ViewsKeeper.Model;
using MediatR;

namespace DocuEye.ViewsKeeper.Application.Queries.GetImageView
{
    /// <summary>
    /// Gets Image view
    /// </summary>
    public class GetImageViewQuery : IRequest<ImageView?>
    {
        /// <summary>
        /// The ID of view
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The ID of view</param>
        /// <param name="workspaceId">The ID of workspace</param>
        public GetImageViewQuery(string id, string workspaceId)
        {
            this.Id = id;
            this.WorkspaceId = workspaceId;
        }
    }
}
