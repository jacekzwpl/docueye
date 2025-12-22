using DocuEye.Infrastructure.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ViewsKeeper.Application.Commands.SaveViewLayout
{
    public class SaveViewLayoutCommand : ICommand
    {
        public string WorkspaceId { get; }
        public string ViewId { get; }
        public string LayoutData { get; }

        public SaveViewLayoutCommand(string workspaceId, string viewId, string layoutData)
        {
            WorkspaceId = workspaceId;
            ViewId = viewId;
            LayoutData = layoutData;
        }
    }
}
