using DocuEye.WorkspacesKeeper.Application.Model;
using System;

namespace DocuEye.WorkspacesKeeper.Model.Maps
{
    public static class WorkspaceMap
    {
        public static FoundedWorkspace MapToFoundedWorkspace(this Workspace source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var dest = new FoundedWorkspace
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description
            };
            return dest;
        }
    }
}
