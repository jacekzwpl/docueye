using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override StructurizrViews VisitViews([NotNull] StructurizrDSLParser.ViewsContext context)
        {
            var views = new StructurizrViews();
            var bodyContext = context.viewsBody();
            return this.VisitViewsBody(bodyContext, views);
        }

        public StructurizrViews VisitViewsBody([NotNull] StructurizrDSLParser.ViewsBodyContext context, StructurizrViews views)
        {
           
            views.Properties = this.VisitPropertiesBlocks(context.properties());

            if (context.children == null)
            {
                return views;
            }

            foreach (var childContext in context.children)
            {
                if (childContext is SystemLandScapeContext)
                {
                    var systemLandScapeContext = (SystemLandScapeContext)childContext;
                    views.SystemLandscapeViews.Add(this.VisitSystemLandScape(systemLandScapeContext));
                }

                if (childContext is SystemContextContext)
                {
                    var systemContextContext = (SystemContextContext)childContext;
                    views.SystemContextViews.Add(this.VisitSystemContext(systemContextContext));
                }
                if (childContext is ContainerViewContext)
                {
                    var containerViewContext = (ContainerViewContext)childContext;
                    views.ContainerViews.Add(this.VisitContainerView(containerViewContext));
                }
                if (childContext is ComponentViewContext)
                {
                    var componentViewContext = (ComponentViewContext)childContext;
                    views.ComponentViews.Add(this.VisitComponentView(componentViewContext));
                }
                if (childContext is DynamicViewContext)
                {
                    var dynamicViewContext = (DynamicViewContext)childContext;
                    views.DynamicViews.Add(this.VisitDynamicView(dynamicViewContext));
                }
                if (childContext is DeploymentViewContext)
                {
                    var deploymentViewContext = (DeploymentViewContext)childContext;
                    views.DeploymentViews.Add(this.VisitDeploymentView(deploymentViewContext));
                }
                if (childContext is CustomViewContext)
                {
                    var customViewContext = (CustomViewContext)childContext;
                    views.CustomViews.Add(this.VisitCustomView(customViewContext));
                }
                if (childContext is ImageViewContext) { 
                    var imageViewContext = (ImageViewContext)childContext;
                    views.ImageViews.Add(this.VisitImageView(imageViewContext));
                }
                if(childContext is FilteredViewContext)
                {
                    var filteredViewContext = (FilteredViewContext)childContext;
                    views.FilteredViews.Add(this.VisitFilteredView(filteredViewContext));
                }

                if(childContext is StylesContext)
                {
                    var stylesContext = (StylesContext)childContext;
                    views.Styles = this.VisitStyles(stylesContext);
                }

                if(childContext is ThemeBlockContext)
                {
                    var themeContext = (ThemeBlockContext)childContext;
                    views.Themes.Add((string)this.VisitThemeBlock(themeContext));
                }

                if (childContext is ThemesBlockContext)
                {
                    var themesContext = (ThemesBlockContext)childContext;
                    views.Themes.AddRange(this.VisitThemesBlock(themesContext));
                }

                if(childContext is BrandingContext)
                {
                    var brandingContext = (BrandingContext)childContext;
                    views.Branding = this.VisitBranding(brandingContext);
                }

                if(childContext is TerminologyContext)
                {
                    var terminologyContext = (TerminologyContext)childContext;
                    views.Terminology = this.VisitTerminology(terminologyContext);
                }

                if (childContext is IncludeFileContext)
                {
                    var includeFileContext = (IncludeFileContext)childContext;
                    this.VisitIncludeFileViews(includeFileContext, views);
                }
            }
            return views;
        }
    }
}
