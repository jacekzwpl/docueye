namespace DocuEye.Structurizr.DSL.Expressions.Model
{
    public class DSLExpression
    {
        public DSLExpressionOperator ExpressionOperator { get; set; } = DSLExpressionOperator.Equal;
        public DSLExpressionType ExpressionType { get; set; } = DSLExpressionType.ElementType;
        public DSLExpressionConnector ExpressionConnector { get; set; } = DSLExpressionConnector.Undefined;
        public string? PropertyName { get; set; } = null!;
        public string? ExpressionValue { get; set; }
    }
}
