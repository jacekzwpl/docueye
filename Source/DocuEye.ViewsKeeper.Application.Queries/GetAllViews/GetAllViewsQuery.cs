using DocuEye.ViewsKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ViewsKeeper.Application.Queries.GetAllViews
{
    public class GetAllViewsQuery : IRequest<IEnumerable<BaseView>>
    {
        public string WorkspaceId { get; set; }

        public GetAllViewsQuery(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
