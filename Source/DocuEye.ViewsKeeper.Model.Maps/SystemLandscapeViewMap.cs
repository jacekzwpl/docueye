using DocuEye.ViewsKeeper.Application.Model;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.ViewsKeeper.Model.Maps
{
    public static class SystemLandscapeViewMap
    {
        public static WorkspaceView MapToWorkspaceView(this SystemLandscapeView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var dest = new WorkspaceView
            {
                Id = source.Id,
                ViewType = source.ViewType,
                Name = string.Format("[{0}]{1}", source.ViewType, source.Title ?? source.Description ?? source.Key)
            };
            return dest;
        }

        public static IEnumerable<WorkspaceView> MapToWorkspaceViews(this IEnumerable<SystemLandscapeView> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<WorkspaceView>();
            foreach (var s in sources) result.Add(s.MapToWorkspaceView());
            return result.ToArray();
        }

        public static ViewWithElement MapToViewWithElement(this SystemLandscapeView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var dest = new ViewWithElement
            {
                Id = source.Id,
                ViewType = source.ViewType,
                Name = string.Format("[{0}]{1}", source.ViewType, source.Title ?? source.Description ?? source.Key)
            };
            return dest;
        }

        public static IEnumerable<ViewWithElement> MapToViewWithElements(this IEnumerable<SystemLandscapeView> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<ViewWithElement>();
            foreach (var s in sources) result.Add(s.MapToViewWithElement());
            return result.ToArray();
        }
    }
}
