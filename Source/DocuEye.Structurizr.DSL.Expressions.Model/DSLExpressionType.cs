using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Model
{
    public enum DSLExpressionType
    {
        ElementType,
        ElementParent,
        ElementTag,
        ElementTechnology,
        ElementProperties,
        ElementAfferentCouplings,
        ElementEfferentCouplings,
        ElementAllCouplings,
        RelationshipTag,
        RelationshipProperties,
        RelationshipSource,
        RelationshipDestination,
        RelationshipAll,
        RelationshipSpecified
    }
}
