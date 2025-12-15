using DocuEye.WorkspaceImporter.Api.Model.Views;
using DocuEye.ViewsKeeper.Model;
using System;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class ViewToImportMap
    {
        public static SystemLandscapeView MapToSystemLandscapeView(this ViewToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new SystemLandscapeView
            {
                Key = source.Key,
                Description = source.Description,
                EnterpriseBoundaryVisible = source.ExternalBoundariesVisible,
                AutomaticLayout = source.AutomaticLayout?.ToAutomaticLayout(),
                PaperSize = source.PaperSize,
                Title = source.Title,
                ViewType = ViewType.SystemLandscapeView,
            };
        }

        public static SystemContextView MapToSystemContextView(this ViewToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new SystemContextView
            {
                Key = source.Key,
                Description = source.Description,
                EnterpriseBoundaryVisible = source.ExternalBoundariesVisible,
                AutomaticLayout = source.AutomaticLayout?.ToAutomaticLayout(),
                PaperSize = source.PaperSize,
                Title = source.Title,
                ViewType = ViewType.SystemContextView
            };
        }

        public static ContainerView MapToContainerView(this ViewToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ContainerView
            {
                Key = source.Key,
                Description = source.Description,
                ExternalSoftwareSystemBoundariesVisible = source.ExternalBoundariesVisible,
                AutomaticLayout = source.AutomaticLayout?.ToAutomaticLayout(),
                PaperSize = source.PaperSize,
                Title = source.Title,
                ViewType = ViewType.ContainerView
            };
        }

        public static ComponentView MapToComponentView(this ViewToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ComponentView
            {
                Key = source.Key,
                Description = source.Description,
                ExternalContainerBoundariesVisible = source.ExternalBoundariesVisible,
                AutomaticLayout = source.AutomaticLayout?.ToAutomaticLayout(),
                PaperSize = source.PaperSize,
                Title = source.Title,
                ViewType = ViewType.ComponentView
            };
        }

        public static DeploymentView MapToDeploymentView(this ViewToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new DeploymentView
            {
                Key = source.Key,
                Description = source.Description,
                AutomaticLayout = source.AutomaticLayout?.ToAutomaticLayout(),
                PaperSize = source.PaperSize,
                Title = source.Title,
                ViewType = ViewType.DeploymentView
            };
        }

        public static ImageView MapToImageView(this ViewToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ImageView
            {
                Key = source.Key,
                Content = source.Content,
                ContentType = source.ContentType,
                Title = source.Title,
                Description = source.Description,
                ViewType = ViewType.ImageView
            };
        }

        public static DynamicView MapToDynamicView(this ViewToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new DynamicView
            {
                Key = source.Key,
                Description = source.Description,
                AutomaticLayout = source.AutomaticLayout?.ToAutomaticLayout(),
                PaperSize = source.PaperSize,
                Title = source.Title,
                ExternalBoundariesVisible = source.ExternalBoundariesVisible,
                ViewType = ViewType.DynamicView
            };
        }

        public static FilteredView MapToFilteredView(this ViewToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new FilteredView
            {
                Key = source.Key,
                Description = source.Description,
                AutomaticLayout = source.AutomaticLayout?.ToAutomaticLayout(),
                PaperSize = source.PaperSize,
                Title = source.Title,
                BaseViewKey = source.BaseViewKey ?? string.Empty,
                Mode = source.Mode,
                Tags = source.Tags,
                ViewType = ViewType.FilteredView
            };
        }
    }
}
