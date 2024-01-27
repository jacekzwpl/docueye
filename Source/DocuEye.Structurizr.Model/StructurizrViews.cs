using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrViews
    {
        /// <summary>
        /// The set of system landscape views.
        /// </summary>
        public IEnumerable<StructurizrSystemLandscapeView>? SystemLandscapeViews { get; set; }
        /// <summary>
        /// The set of system context views.
        /// </summary>
        public IEnumerable<StructurizrSystemContextView>? SystemContextViews { get; set; }
        /// <summary>
        /// The set of container views.
        /// </summary>
        public IEnumerable<StructurizrContainerView>? ContainerViews { get; set; }
        /// <summary>
        /// The set of component views.
        /// </summary>
        public IEnumerable<StructurizrComponentView>? ComponentViews { get; set; }
        /// <summary>
        /// The set of dynamic views.
        /// </summary>
        public IEnumerable<StructurizrDynamicView>? DynamicViews { get; set; }
        /// <summary>
        /// The set of deployment views.
        /// </summary>
        public IEnumerable<StructurizrDeploymentView>? DeploymentViews { get; set; }
        /// <summary>
        /// The set of filtered views.
        /// </summary>
        public IEnumerable<StructurizrFilteredView>? FilteredViews { get; set; }

        public IEnumerable<StructurizrImageView>? ImageViews { get; set; }

        public StructurizrConfiguration? Configuration { get; set; }
    }
}
