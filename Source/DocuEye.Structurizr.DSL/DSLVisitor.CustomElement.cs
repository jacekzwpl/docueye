using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override object VisitElement([NotNull] ElementContext context)
        {
            string identifier;
            var identifierContext = context.identifier();
            if (identifierContext == null)
            {
                identifier = Guid.NewGuid().ToString();
            }
            else
            {
                identifier = identifierContext.GetText().Trim('"');
            }

            var element = new StructurizrCustomElement(identifier, new string[] {
                "Element"
            });

            var nameContext = context.name();
            if (nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Name is required for element at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var name = nameContext.GetText().Trim('"');
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(
                    string.Format(
                        "Name is required for element at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            element.Name = name;

            var descriptionContext = context.description();
            if (descriptionContext != null)
            {
                var description = descriptionContext.GetText().Trim('"');
                element.Description = description;
            }

            var metadataContext = context.metadata();
            if (metadataContext != null)
            {
                var metadata = metadataContext.GetText().Trim('"');
                element.Metadata = metadata;
            }

            var tagsContext = context.tags();
            if (tagsContext != null)
            {
                var tags = (string[])this.VisitTags(tagsContext);
                element.Tags.AddRange(tags);
            }

            var bodyContext = context.modelElementBody();
            if (bodyContext != null)
            {
                var modelElement = this.VisitModelElementBody(bodyContext, element.ToModelElement());
                element.FromModelElement(modelElement);
                /*
                var systemBody = this.VisitElementBody(bodyContext, element.Identifier);
                element.Tags.AddRange(systemBody.Tags);
                if (element.Description == null)
                {
                    element.Description = systemBody.Description;
                }
                element.Properties = systemBody.Properties;
                element.Url = systemBody.Url;
                element.Perspectives = systemBody.Perspectives;
                */
            }

            return element;
        }

    }
}
