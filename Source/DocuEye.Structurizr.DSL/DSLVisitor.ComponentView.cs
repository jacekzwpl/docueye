using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override StructurizrComponentView VisitComponentView([NotNull] StructurizrDSLParser.ComponentViewContext context)
        {
            string elementIdentifier;

            var identifierReference = context.identifierReference();
            if (identifierReference != null)
            {
                elementIdentifier = identifierReference.GetText().Trim('"');
            }
            else
            {
                throw new Exception(
                    string.Format(
                        "Container identifier is missing at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

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
            if (descriptionContext == null)
            {
                descritpiton = null;
            }
            else
            {
                descritpiton = descriptionContext.GetText().Trim('"');
            }

            var view = new StructurizrComponentView(elementIdentifier, identifier, descritpiton);

            var viewBodyContext = context.componentViewBody();
            if (viewBodyContext != null)
            {
                var viewBody = this.VisitComponentViewBody(viewBodyContext);
                view.Properties = viewBody.Properties;
                view.Title = viewBody.Title;
                if (view.Description == null)
                {
                    view.Description = viewBody.Description;
                }
                view.Include = viewBody.Include;
                view.Exclude = viewBody.Exclude;
                view.Animation = viewBody.Animation;
                view.AutomaticLayout = viewBody.AutomaticLayout;
                view.Default = viewBody.Default;

            }
            return view;
        }

        public override StructurizrComponentView VisitComponentViewBody([NotNull] StructurizrDSLParser.ComponentViewBodyContext context)
        {
            var view = new StructurizrComponentView("", "");
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
