using System;

namespace DocuEye.Linter.Expressions.Model
{
    public class LinterExpression
    {
        public LinterExpressionOperator Operator { get; set; } = LinterExpressionOperator.Equal;
        public LinterExpressionConnector Connector { get; set; } = LinterExpressionConnector.Undefined;
        public LinterExpressionValue? Value { get; set; } = null;
    }
}
