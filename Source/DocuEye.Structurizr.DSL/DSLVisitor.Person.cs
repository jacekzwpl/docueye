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
        public override object VisitPerson([NotNull] PersonContext context)
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

            var person = new StructurizrPerson(identifier, new string[] { "Element", "Person" });

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
            person.Name = name;


            var descriptionContext = context.description();
            if (descriptionContext != null)
            {
                var description = descriptionContext.GetText().Trim('"');
                person.Description = description;
            }

            var tagsContext = context.tags();
            if (tagsContext != null)
            {
                var tags = (string[])this.VisitTags(tagsContext);
                person.Tags.AddRange(tags);
            }

            var modelElementBodyContext = context.modelElementBody();
            if (modelElementBodyContext != null)
            {
                var element = this.VisitModelElementBody(modelElementBodyContext, person.ToModelElement());
                person.FromModelElement(element);
                /*
                var personBody = (StructurizrPerson)this.VisitPersonBody(personBodyContext, person.Identifier);
                person.Tags.AddRange(personBody.Tags);
                if (person.Description == null)
                {
                    person.Description = personBody.Description;
                }
                person.Properties = personBody.Properties;
                person.Url = personBody.Url;
                person.Perspectives = personBody.Perspectives;*/
            }

            return person;
        }

        
    }
}
