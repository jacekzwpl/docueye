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
        public override StructurizrStyles VisitStyles([NotNull] StructurizrDSLParser.StylesContext context)
        {
            var styles = new StructurizrStyles();
            var elementStyleContexts = context.elementStyle();
            if (elementStyleContexts != null)
            {
                foreach (var elementStyleContext in elementStyleContexts)
                {
                    var elementStyle = this.VisitElementStyle(elementStyleContext);
                    styles.ElementStyles.Add(elementStyle);
                }
            }

            var relationshipStyleContexts = context.relationshipStyle();
            if (relationshipStyleContexts != null)
            {
                foreach (var relationshipStyleContext in relationshipStyleContexts)
                {
                    var relationshipStyle = this.VisitRelationshipStyle(relationshipStyleContext);
                    styles.RelationshipStyles.Add(relationshipStyle);
                }
            }

            return styles;
        }
    }
}
