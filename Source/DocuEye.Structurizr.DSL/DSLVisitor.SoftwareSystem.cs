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
        public override object VisitSoftwareSystem([NotNull] SoftwareSystemContext context)
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

            var element = new StructurizrSoftwareSystem(identifier, new string[] { 
                "Element", "Software System" 
            });

            var nameContext = context.name();
            if (nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Name is required for person at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var name = nameContext.GetText().Trim('"');
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(
                    string.Format(
                        "Name is required for person at {0}:{1}",
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
                var systemBody = this.VisitSoftwareSystemBody(bodyContext, element.Identifier);
                element.Tags.AddRange(systemBody.Tags);
                if (element.Description == null)
                {
                    element.Description = systemBody.Description;
                }
                element.Properties = systemBody.Properties;
                element.Url = systemBody.Url;
                element.Perspectives = systemBody.Perspectives;
                element.Adrs = systemBody.Adrs;
                element.Docs = systemBody.Docs;*/
            }

            return element;
        }

        
    }
}
