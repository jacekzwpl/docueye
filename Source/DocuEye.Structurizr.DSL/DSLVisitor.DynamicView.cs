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
        public override StructurizrDynamicView VisitDynamicView([NotNull] StructurizrDSLParser.DynamicViewContext context)
        {
            string elementIdentifier;

            var identifierReference = context.dynamicViewContext();
            if (identifierReference != null)
            {
                elementIdentifier = identifierReference.GetText().Trim('"');
            }
            else
            {
                throw new Exception(
                    string.Format(
                        "Context identifier is missing at {0}:{1}",
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
            var view = new StructurizrDynamicView(elementIdentifier, identifier, descritpiton);

            var viewBodyContext = context.dynamicViewBody();
            if (viewBodyContext != null) {
                var viewBody = this.VisitDynamicViewBody(viewBodyContext);
                view.Properties = viewBody.Properties;
                view.Title = viewBody.Title;
                view.AutomaticLayout = viewBody.AutomaticLayout;
                view.Default = viewBody.Default;
                view.Relationships = viewBody.Relationships;
            }

            return view;
        }

        public override StructurizrDynamicView VisitDynamicViewBody([NotNull] StructurizrDSLParser.DynamicViewBodyContext context)
        {

            var view = new StructurizrDynamicView("", "");
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
            int currentOrder = 1;
            foreach (var child in context.children)
            {
                if (child is StructurizrDSLParser.DynamicViewRelationshipContext)
                {
                    var relationship = this.VisitDynamicViewRelationship((StructurizrDSLParser.DynamicViewRelationshipContext)child);
                    if (relationship.Order == 0)
                    {
                        relationship.Order = currentOrder;
                        currentOrder++;
                    }
                    view.Relationships.Add(relationship);
                }
                if (child is StructurizrDSLParser.DynamicViewRelationshipGroupContext)
                {
                    var groupRelationships = this.VisitDynamicViewRelationshipGroup((StructurizrDSLParser.DynamicViewRelationshipGroupContext)child, currentOrder);
                    view.Relationships.AddRange(groupRelationships);
                    currentOrder++;
                }
            }
            return view;
        }
    }
}
