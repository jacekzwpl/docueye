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

        public override object VisitAutoLayoutBlock([NotNull] StructurizrDSLParser.AutoLayoutBlockContext context)
        {
            var autoLayout = new StructurizrAutoLayout();
            var rankDirectionContext = context.rankDirection();
            if(rankDirectionContext != null)
            {
                var rankDirection = rankDirectionContext.GetText().Trim('"');
                if(rankDirection == String.Empty)
                {
                    rankDirection = "tb";
                }
                else if(!new string[] { "tb", "bt", "lr", "rl" }.Contains(rankDirection))
                {
                    throw new Exception(
                    string.Format(
                        "Rank direction must be one of 'tb', 'bt', 'lr', 'rl' at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
                }
                autoLayout.RankDirection = rankDirection;
            }

            var rankSeparationContext = context.rankSeparation();
            if (rankSeparationContext != null)
            {
                int rankSeparation;
                if (!int.TryParse(rankSeparationContext.GetText().Trim('"'), out rankSeparation))
                {
                    throw new Exception(
                    string.Format(
                        "Rank separation must be an integer at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
                }
                autoLayout.RankSeparation = rankSeparation;
            }

            var nodeSeparationContext = context.nodeSeparation();
            if (nodeSeparationContext != null)
            {
                int nodeSeparation;
                if (!int.TryParse(nodeSeparationContext.GetText().Trim('"'), out nodeSeparation))
                {
                    throw new Exception(
                    string.Format(
                        "Node separation must be an integer at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
                }
                autoLayout.NodeSeparation = nodeSeparation;
            }
            return autoLayout;
        }
    }
}
