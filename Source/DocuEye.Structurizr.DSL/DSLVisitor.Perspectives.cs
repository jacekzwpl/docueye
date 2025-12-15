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
        public IEnumerable<StructurizrPerspective>? VisitPerspectives(PerspectivesContext[]? contexts)
        {
            if (contexts == null)
            {
                return null;
            }
            if (contexts.Length > 1)
            {
                throw new Exception(
                    string.Format(
                        "Duplicate perspectives block at {0}:{1}",
                        contexts[1].Start.Line,
                        contexts[1].Start.Column));
            }
            if (contexts.Length == 1)
            {
                return (IEnumerable<StructurizrPerspective>)this.VisitPerspectives(contexts[0]);
            }
            return null;
        }

        public override object VisitPerspectives([NotNull] PerspectivesContext context)
        {
            var perspectives = new List<StructurizrPerspective>();

            var perspectiveContexts = context.perspective();

            foreach (var perspectiveContext in perspectiveContexts)
            {
                perspectives.Add((StructurizrPerspective)this.VisitPerspective(perspectiveContext));
            }

            return perspectives;
        }

        public override object VisitPerspective([NotNull] PerspectiveContext context)
        {
            var perspective = new StructurizrPerspective();
            var nameContext = context.name();
            if (nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Name is required for perspective at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            perspective.Name = nameContext.GetText().Trim('"');

            var descriptionContext = context.description();
            if (descriptionContext != null)
            {
                perspective.Description = descriptionContext.GetText().Trim('"');
            }

            var valueContext = context.value();
            if (valueContext != null)
            {
                perspective.Value = valueContext.GetText().Trim('"');
            }

            return perspective;
        }
    }
}
