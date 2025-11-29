using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override object VisitAnimation([NotNull] StructurizrDSLParser.AnimationContext context)
        {
            var bodyContext = context.animationBody();
            if(bodyContext != null)
            {
                return this.VisitAnimationBody(bodyContext);
            }
            return new List<IEnumerable<string>>();
        }

        public override object VisitAnimationBody([NotNull] StructurizrDSLParser.AnimationBodyContext context)
        {
            var steps = new List<IEnumerable<string>>();
            var stepContexts = context.animationStep();
            foreach (var stepContext in stepContexts)
            {
                var step = (IEnumerable<string>)this.VisitAnimationStep(stepContext);
                steps.Add(step);
            }
            return steps;
        }

        public override object VisitAnimationStep([NotNull] StructurizrDSLParser.AnimationStepContext context)
        {
            var identifiers = new List<string>();
            var identifierContexts = context.identifier();
            foreach (var identifierContext in identifierContexts)
            {
                var identifier = identifierContext.GetText().Trim('"');
                identifiers.Add(identifier);
            }
            return identifiers;
        }
    }
}
