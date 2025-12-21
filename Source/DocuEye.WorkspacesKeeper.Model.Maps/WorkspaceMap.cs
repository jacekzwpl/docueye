using DocuEye.WorkspacesKeeper.Application.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

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

        public static IEnumerable<FoundedWorkspace> MapToFoundedWorkspaces(this IEnumerable<Workspace> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<FoundedWorkspace>();
            foreach (var s in sources) result.Add(s.MapToFoundedWorkspace());
            return result.ToArray();
        }
    }
}
