using DocuEye.ViewsKeeper.Api.Model;
using DocuEye.ViewsKeeper.Application.Model;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ViewsKeeper.Model.Maps
{
    public static class ContainerViewMap
    {
        public static WorkspaceView MapToWorkspaceView(this ContainerView source)
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

        public static IEnumerable<WorkspaceView> MapToWorkspaceViews(this IEnumerable<ContainerView> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<WorkspaceView>();
            foreach (var s in sources) result.Add(s.MapToWorkspaceView());
            return result.ToArray();
        }

        public static ViewWithElement MapToViewWithElement(this ContainerView source) { 
            if (source == null) throw new ArgumentNullException( nameof(source));
            return new ViewWithElement
            {
                Id = source.Id,
                ViewType = source.ViewType,
                Name = string.Format("[{0}]{1}", source.ViewType, source.Title ?? source.Description ?? source.Key)
            };
        }

        public static IEnumerable<ViewWithElement> MapToViewWithElements(this IEnumerable<ContainerView> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<ViewWithElement>();
            foreach (var s in sources) result.Add(s.MapToViewWithElement());
            return result.ToArray();
        }

        public static ContainerViewDiagram MapToContainerViewDiagram(this ContainerView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var dest = new ContainerViewDiagram
            {
                Id = source.Id,
                ViewType = source.ViewType,
                Title = source.Title,
                Description = source.Description,
                Key = source.Key,
                SoftwareSystemId = source.SoftwareSystemId,
                ExternalSoftwareSystemBoundariesVisible = source.ExternalSoftwareSystemBoundariesVisible,
                AutomaticLayout = source.AutomaticLayout,
                Elements = source.Elements,
                Relationships = source.Relationships,
                PaperSize = source.PaperSize,
                WorkspaceId = source.WorkspaceId
            };
            return dest;
        }
    }
}
