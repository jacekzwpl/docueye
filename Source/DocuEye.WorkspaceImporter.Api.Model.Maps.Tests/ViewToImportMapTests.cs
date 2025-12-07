using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Maps;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using DocuEye.ViewsKeeper.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class ViewToImportMapTests
    {
        private readonly ViewToImport source = new ViewToImport
        {
            Key = "key",
            AutomaticLayout = new ViewAutomaticLayout()
            {
                EdgeSeparation = 10,
                Implementation = "implem",
                NodeSeparation = 20,
                RankDirection = "rankDir",
                RankSeparation = 30,
                Vertices = true
            },
            BaseViewKey = "baseView",
            Content = "content",
            ContentType = "contentType",
            Description = "description",
            Elements = new List<ElementInViewToImport>(),
            ExternalBoundariesVisible = true,
            Mode = "mode",
            PaperSize = "A4",
            Relationships = new List<RelationshipInViewToImport>(),
            StructurizrElementId = "structId",
            Tags = new List<string> { "tag1", "tag2" },
            Title = "title",
            ViewType = "SomeViewType"

        };

        [Test]
        public void Mapping_ViewToImport_To_SystemLandscapeView_Works()
        {

            var result = source.ToSystemLandscapeView();

            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(SystemLandscapeView.AutomaticLayout), 
                    nameof(SystemLandscapeView.Elements), 
                    nameof(SystemLandscapeView.Relationships),
                    nameof(SystemLandscapeView.WorkspaceId),
                    nameof(SystemLandscapeView.Id)
                },
                customSourceResolvers: new Dictionary<string, Func<ViewToImport, object?>>
                {
                    { nameof(SystemLandscapeView.ViewType), s => ViewType.SystemLandscapeView },
                    { nameof(SystemLandscapeView.EnterpriseBoundaryVisible), s => s.ExternalBoundariesVisible }
                }
            );
        }

        [Test]
        public void Mapping_ViewToImport_To_SystemContextView_Works()
        {
            var result = source.ToSystemContextView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(SystemContextView.AutomaticLayout), 
                    nameof(SystemContextView.Elements), 
                    nameof(SystemContextView.Relationships),
                    nameof(SystemContextView.SoftwareSystemId),
                    nameof(SystemContextView.Id),
                    nameof(SystemContextView.WorkspaceId)
                },
                customSourceResolvers: new Dictionary<string, Func<ViewToImport, object?>>
                {
                    { nameof(SystemContextView.ViewType), s => ViewType.SystemContextView },
                    { nameof(SystemContextView.EnterpriseBoundaryVisible), s => s.ExternalBoundariesVisible }
                }
            );
        }

        [Test]
        public void Mapping_ViewToImport_To_ContainerView_Works()
        {

            var result = source.ToContainerView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(ContainerView.AutomaticLayout), 
                    nameof(ContainerView.Elements), 
                    nameof(ContainerView.Relationships),
                    nameof(ContainerView.SoftwareSystemId),
                    nameof(ContainerView.Id),
                    nameof(ContainerView.WorkspaceId)
                },
                customSourceResolvers: new Dictionary<string, Func<ViewToImport, object?>>
                {
                    { nameof(ContainerView.ViewType), s => ViewType.ContainerView },
                    { nameof(ContainerView.ExternalSoftwareSystemBoundariesVisible), s => s.ExternalBoundariesVisible }
                }
            );
        }

        [Test]
        public void Mapping_ViewToImport_To_ComponentView_Works()
        {

            var result = source.ToComponentView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(ComponentView.ContainerId),
                    nameof(ComponentView.AutomaticLayout),
                    nameof(ComponentView.Elements), 
                    nameof(ComponentView.Relationships),
                    nameof(ComponentView.WorkspaceId),
                    nameof(ComponentView.Id)
                },
                customSourceResolvers: new Dictionary<string, Func<ViewToImport, object?>>
                {
                    { nameof(ComponentView.ViewType), s => ViewType.ComponentView },
                    { nameof(ComponentView.ExternalContainerBoundariesVisible), s => s.ExternalBoundariesVisible }
                }
            );
        }

        [Test]
        public void Mapping_ViewToImport_To_DeploymentView_Works()
        {

            var result = source.ToDeploymentView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(DeploymentView.AutomaticLayout), 
                    nameof(DeploymentView.Elements), 
                    nameof(DeploymentView.Relationships),
                    nameof(DeploymentView.WorkspaceId),
                    nameof(DeploymentView.Id),
                    nameof(DeploymentView.SoftwareSystemId)
                },
                customSourceResolvers: new Dictionary<string, Func<ViewToImport, object?>>
                {
                    { nameof(DeploymentView.ViewType), s => ViewType.DeploymentView }
                }
            );
        }

        [Test]
        public void Mapping_ViewToImport_To_ImageView_Works()
        {

            var result = source.ToImageView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(ImageView.WorkspaceId), 
                    nameof(ImageView.Id),
                    nameof(ImageView.ElementId)
                },
                customSourceResolvers: new Dictionary<string, Func<ViewToImport, object?>>
                {
                    { nameof(ImageView.ViewType), s => ViewType.ImageView }
                }
            );
        }

        [Test]
        public void Mapping_ViewToImport_To_DynamicView_Works()
        {

            var result = source.ToDynamicView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(DynamicView.AutomaticLayout), 
                    nameof(DynamicView.Elements), 
                    nameof(DynamicView.Relationships),
                    nameof(DynamicView.WorkspaceId),
                    nameof(DynamicView.Id),
                    nameof(DynamicView.ElementId)
                },
                customSourceResolvers: new Dictionary<string, Func<ViewToImport, object?>>
                {
                    { nameof(DynamicView.ViewType), s => ViewType.DynamicView }
                }
            );
        }

        [Test]
        public void Mapping_ViewToImport_To_FilteredView_Works()
        {
     
            var result = source.ToFilteredView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(FilteredView.AutomaticLayout), 
                    nameof(FilteredView.Elements), 
                    nameof(FilteredView.Relationships),
                    nameof(FilteredView.WorkspaceId), 
                    nameof(FilteredView.Id)
                },
                customSourceResolvers: new Dictionary<string, Func<ViewToImport, object?>>
                {
                    { nameof(FilteredView.ViewType), s => ViewType.FilteredView }
                }
            );
        }
    }
}
