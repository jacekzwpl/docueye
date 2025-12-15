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
        public override StructurizrCustomView VisitCustomView([NotNull] StructurizrDSLParser.CustomViewContext context)
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

            string? title;
            var titleContext = context.title();
            if (titleContext == null)
            {
                title = null;
            }
            else
            {
                title = titleContext.GetText().Trim('"');
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

            var view = new StructurizrCustomView(identifier, title, descritpiton);
            var viewBodyContext = context.customViewBody();
            if (viewBodyContext != null)
            {
                var viewBody = this.VisitCustomViewBody(viewBodyContext);
                view.Properties = viewBody.Properties;
                if(view.Title == null)
                {
                    view.Title = viewBody.Title;
                }
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

        public override StructurizrCustomView VisitCustomViewBody([NotNull] StructurizrDSLParser.CustomViewBodyContext context)
        {
            var view = new StructurizrCustomView("");
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
