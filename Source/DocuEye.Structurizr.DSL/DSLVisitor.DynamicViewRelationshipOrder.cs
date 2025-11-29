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
        public override object VisitDynamicViewRelationshipOrder([NotNull] StructurizrDSLParser.DynamicViewRelationshipOrderContext context)
        {
            var orderString = context.WORD().GetText();
            if(orderString.EndsWith(":"))
            {
                orderString = orderString.Substring(0, orderString.Length - 1);
            }
            int order = 0;
            if(int.TryParse(orderString, out order))
            {
                if(order <= 0)
                {
                    throw new Exception(
                        string.Format(
                            "Order should be greater than 0 but got value '{0}' at {1}:{2}",
                            orderString,
                            context.Start.Line,
                            context.Start.Column));
                }
                return order;
            }
            throw new Exception(
                    string.Format(
                        "Order should be the valid integer but got value '{0}' at {1}:{2}",
                        orderString,
                        context.Start.Line,
                        context.Start.Column));
        }
    }
}
