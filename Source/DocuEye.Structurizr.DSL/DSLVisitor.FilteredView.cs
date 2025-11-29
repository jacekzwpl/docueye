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
        public override StructurizrFilteredView VisitFilteredView([NotNull] StructurizrDSLParser.FilteredViewContext context)
        {
            string baseViewIdentifier;

            var identifierReference = context.name();
            if (identifierReference != null)
            {
                baseViewIdentifier = identifierReference.GetText().Trim('"');
            }
            else
            {
                throw new Exception(
                    string.Format(
                        "Base View identifier is missing at {0}:{1}",
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

            var filterModeContext = context.filteredViewMode();
            string filterMode = "include";
            if (filterModeContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Filter mode is missing at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }else
            {
                filterMode = filterModeContext.INCLUDE() != null ? "include" : "exclude";
            }

            var tagsContext = context.tags();
            List<string> tags = new List<string>();
            if (tagsContext != null)
            {
                tags.AddRange((string[])this.VisitTags(tagsContext));
            }

            var view = new StructurizrFilteredView(identifier, baseViewIdentifier)
            {
                Description = descritpiton,
                FilterMode = filterMode,
                Tags = tags
            };

            var viewBodyContext = context.filteredViewBody();
            if (viewBodyContext != null)
            {
                var viewBody = this.VisitFilteredViewBody(viewBodyContext);
                view.Properties = viewBody.Properties;
                view.Title = viewBody.Title;
                if (view.Description == null)
                {
                    view.Description = viewBody.Description;
                }
                view.Default = viewBody.Default;

            }
            return view;
        }

        public override StructurizrFilteredView VisitFilteredViewBody([NotNull] StructurizrDSLParser.FilteredViewBodyContext context)
        {
            var view = new StructurizrFilteredView("", "");
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

            var defaultContext = context.defaultBlock();
            if (defaultContext != null && defaultContext.Length > 0)
            {
                view.Default = true;
            }


            return view;
        }
    }
}
