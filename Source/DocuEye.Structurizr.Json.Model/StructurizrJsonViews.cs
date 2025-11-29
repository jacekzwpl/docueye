using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonViews
    {
        /// <summary>
        /// The set of system landscape views.
        /// </summary>
        public IEnumerable<StructurizrJsonSystemLandscapeView>? SystemLandscapeViews { get; set; }
        /// <summary>
        /// The set of system context views.
        /// </summary>
        public IEnumerable<StructurizrJsonSystemContextView>? SystemContextViews { get; set; }
        /// <summary>
        /// The set of container views.
        /// </summary>
        public IEnumerable<StructurizrJsonContainerView>? ContainerViews { get; set; }
        /// <summary>
        /// The set of component views.
        /// </summary>
        public IEnumerable<StructurizrJsonComponentView>? ComponentViews { get; set; }
        /// <summary>
        /// The set of dynamic views.
        /// </summary>
        public IEnumerable<StructurizrJsonDynamicView>? DynamicViews { get; set; }
        /// <summary>
        /// The set of deployment views.
        /// </summary>
        public IEnumerable<StructurizrJsonDeploymentView>? DeploymentViews { get; set; }
        /// <summary>
        /// The set of filtered views.
        /// </summary>
        public IEnumerable<StructurizrJsonFilteredView>? FilteredViews { get; set; }

        public IEnumerable<StructurizrJsonImageView>? ImageViews { get; set; }

        public IEnumerable<StructurizrJsonCustomView>? CustomViews { get; set; }

        public StructurizrJsonConfiguration? Configuration { get; set; }
    }
}
