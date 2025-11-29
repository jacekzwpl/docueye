// Generated from c:/nCode/Parser/DocuEye.Structurizr.DSL.Expressions/StructurizrDSLExpressions.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link StructurizrDSLExpressionsParser}.
 */
public interface StructurizrDSLExpressionsListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link StructurizrDSLExpressionsParser#generalExpression}.
	 * @param ctx the parse tree
	 */
	void enterGeneralExpression(StructurizrDSLExpressionsParser.GeneralExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link StructurizrDSLExpressionsParser#generalExpression}.
	 * @param ctx the parse tree
	 */
	void exitGeneralExpression(StructurizrDSLExpressionsParser.GeneralExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link StructurizrDSLExpressionsParser#elementTypeExpr}.
	 * @param ctx the parse tree
	 */
	void enterElementTypeExpr(StructurizrDSLExpressionsParser.ElementTypeExprContext ctx);
	/**
	 * Exit a parse tree produced by {@link StructurizrDSLExpressionsParser#elementTypeExpr}.
	 * @param ctx the parse tree
	 */
	void exitElementTypeExpr(StructurizrDSLExpressionsParser.ElementTypeExprContext ctx);
	/**
	 * Enter a parse tree produced by {@link StructurizrDSLExpressionsParser#elementTypeValue}.
	 * @param ctx the parse tree
	 */
	void enterElementTypeValue(StructurizrDSLExpressionsParser.ElementTypeValueContext ctx);
	/**
	 * Exit a parse tree produced by {@link StructurizrDSLExpressionsParser#elementTypeValue}.
	 * @param ctx the parse tree
	 */
	void exitElementTypeValue(StructurizrDSLExpressionsParser.ElementTypeValueContext ctx);
}