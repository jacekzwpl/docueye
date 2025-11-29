using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System.ComponentModel;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {

        public override StructurizrSystemLandscapeView VisitSystemLandScape([NotNull] StructurizrDSLParser.SystemLandScapeContext context)
        {
            string identifier;
            var identifierContext = context.key();
            if (identifierContext == null)
            {
                identifier = Guid.NewGuid().ToString();
            }
            else
            {
                identifier = identifierContext.GetText().Trim('"');
            }

            string? descritpiton;
            var descriptionContext = context.description();
            if(descriptionContext == null)
            {
                descritpiton = null;
            }
            else
            {
                descritpiton = descriptionContext.GetText().Trim('"');
            }

            var systemLandscapeView = new StructurizrSystemLandscapeView(identifier, descritpiton);

            var viewBodyContext = context.systemLandScapeBody();
            if(viewBodyContext != null)
            {
                var viewBody = this.VisitSystemLandScapeBody(viewBodyContext);
                systemLandscapeView.Properties = viewBody.Properties;
                systemLandscapeView.Title = viewBody.Title;
                if(systemLandscapeView.Description == null)
                {
                    systemLandscapeView.Description = viewBody.Description;
                }
                systemLandscapeView.Include = viewBody.Include;
                systemLandscapeView.Exclude = viewBody.Exclude;
                systemLandscapeView.Animation = viewBody.Animation;
                systemLandscapeView.AutomaticLayout = viewBody.AutomaticLayout;
                systemLandscapeView.Default = viewBody.Default;

            }
            return systemLandscapeView;
        }

        public override StructurizrSystemLandscapeView VisitSystemLandScapeBody([NotNull] StructurizrDSLParser.SystemLandScapeBodyContext context)
        {
            var view = new StructurizrSystemLandscapeView("");
            var propertiesBlock = context.properties();
            if (propertiesBlock != null)
            {
                view.Properties = this.VisitPropertiesBlocks(propertiesBlock);
            }
            var titleBlock = context.titleBlock();
            if (titleBlock != null)
            {
                view.Title = this.VisitTitleBlocks(titleBlock);
            }

            var descriptionBlocks = context.descriptionBlock();
            view.Description = this.VisitDescriptionBlocks(descriptionBlocks);

            var includeContext = context.includeBlock();
            view.Include = this.VisitIncludeBlocks(includeContext);

            var excludeContext = context.excludeBlock();
            view.Exclude = this.VisitExcludeBlocks(excludeContext);
            

            var animationContext = context.animation();
            if (animationContext != null && animationContext.Length > 0)
            {
                view.Animation = (IEnumerable<IEnumerable<string>>)this.VisitAnimation(animationContext.First());
            }

            var autoLayoutContext = context.autoLayoutBlock();
            if (autoLayoutContext != null && autoLayoutContext.Length > 0)
            {
                view.AutomaticLayout = (StructurizrAutoLayout)this.VisitAutoLayoutBlock(autoLayoutContext.First());
            }

            var defaultContext = context.defaultBlock();
            if (defaultContext != null && defaultContext.Length > 0)
            {
                view.Default = true;
            }


            return view;
        }
    }
}
