using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrViews
    {
        public List<StructurizrSystemLandscapeView> SystemLandscapeViews { get; set; } = new List<StructurizrSystemLandscapeView>();
        public List<StructurizrSystemContextView> SystemContextViews { get; set; } = new List<StructurizrSystemContextView>();
        public List<StructurizrContainerView> ContainerViews { get; set; } = new List<StructurizrContainerView>();
        public List<StructurizrComponentView> ComponentViews { get; set; } = new List<StructurizrComponentView>();
        public List<StructurizrDynamicView> DynamicViews { get; set; } = new List<StructurizrDynamicView>();
        public List<StructurizrFilteredView> FilteredViews { get; set; } = new List<StructurizrFilteredView>();
        public List<StructurizrDeploymentView> DeploymentViews { get; set; } = new List<StructurizrDeploymentView>();
        public List<StructurizrCustomView> CustomViews { get; set; } = new List<StructurizrCustomView>();
        public List<StructurizrImageView> ImageViews { get; set; } = new List<StructurizrImageView>();

        public StructurizrStyles? Styles { get; set; }
        public List<string> Themes { get; set; } = new List<string>();
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public StructurizrBranding? Branding { get; set; }
        public StructurizrTerminology? Terminology { get; set; }
    }
}
