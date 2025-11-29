// Generated from c:/nCode/Parser/DocuEye.Structurizr.DSL.Expressions/StructurizrDSLExpressions.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class StructurizrDSLExpressionsParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		ELEMENT_TYPE=1, VALUE=2, OR=3, AND=4, WILDCARD=5, DOT=6, EQUALS=7, WHITESPACE=8;
	public static final int
		RULE_generalExpression = 0, RULE_elementTypeExpr = 1, RULE_elementTypeValue = 2;
	private static String[] makeRuleNames() {
		return new String[] {
			"generalExpression", "elementTypeExpr", "elementTypeValue"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, null, null, "'||'", "'&&'", "'*'", "'.'", "'=='"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "ELEMENT_TYPE", "VALUE", "OR", "AND", "WILDCARD", "DOT", "EQUALS", 
			"WHITESPACE"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "StructurizrDSLExpressions.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public StructurizrDSLExpressionsParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class GeneralExpressionContext extends ParserRuleContext {
		public List<ElementTypeExprContext> elementTypeExpr() {
			return getRuleContexts(ElementTypeExprContext.class);
		}
		public ElementTypeExprContext elementTypeExpr(int i) {
			return getRuleContext(ElementTypeExprContext.class,i);
		}
		public List<TerminalNode> AND() { return getTokens(StructurizrDSLExpressionsParser.AND); }
		public TerminalNode AND(int i) {
			return getToken(StructurizrDSLExpressionsParser.AND, i);
		}
		public List<TerminalNode> OR() { return getTokens(StructurizrDSLExpressionsParser.OR); }
		public TerminalNode OR(int i) {
			return getToken(StructurizrDSLExpressionsParser.OR, i);
		}
		public GeneralExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_generalExpression; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof StructurizrDSLExpressionsListener ) ((StructurizrDSLExpressionsListener)listener).enterGeneralExpression(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof StructurizrDSLExpressionsListener ) ((StructurizrDSLExpressionsListener)listener).exitGeneralExpression(this);
		}
	}

	public final GeneralExpressionContext generalExpression() throws RecognitionException {
		GeneralExpressionContext _localctx = new GeneralExpressionContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_generalExpression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(11);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & 26L) != 0)) {
				{
				setState(9);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case ELEMENT_TYPE:
					{
					setState(6);
					elementTypeExpr();
					}
					break;
				case AND:
					{
					setState(7);
					match(AND);
					}
					break;
				case OR:
					{
					setState(8);
					match(OR);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(13);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ElementTypeExprContext extends ParserRuleContext {
		public TerminalNode ELEMENT_TYPE() { return getToken(StructurizrDSLExpressionsParser.ELEMENT_TYPE, 0); }
		public TerminalNode EQUALS() { return getToken(StructurizrDSLExpressionsParser.EQUALS, 0); }
		public ElementTypeValueContext elementTypeValue() {
			return getRuleContext(ElementTypeValueContext.class,0);
		}
		public ElementTypeExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_elementTypeExpr; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof StructurizrDSLExpressionsListener ) ((StructurizrDSLExpressionsListener)listener).enterElementTypeExpr(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof StructurizrDSLExpressionsListener ) ((StructurizrDSLExpressionsListener)listener).exitElementTypeExpr(this);
		}
	}

	public final ElementTypeExprContext elementTypeExpr() throws RecognitionException {
		ElementTypeExprContext _localctx = new ElementTypeExprContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_elementTypeExpr);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(14);
			match(ELEMENT_TYPE);
			setState(15);
			match(EQUALS);
			setState(16);
			elementTypeValue();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ElementTypeValueContext extends ParserRuleContext {
		public TerminalNode VALUE() { return getToken(StructurizrDSLExpressionsParser.VALUE, 0); }
		public ElementTypeValueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_elementTypeValue; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof StructurizrDSLExpressionsListener ) ((StructurizrDSLExpressionsListener)listener).enterElementTypeValue(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof StructurizrDSLExpressionsListener ) ((StructurizrDSLExpressionsListener)listener).exitElementTypeValue(this);
		}
	}

	public final ElementTypeValueContext elementTypeValue() throws RecognitionException {
		ElementTypeValueContext _localctx = new ElementTypeValueContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_elementTypeValue);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(18);
			match(VALUE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\u0004\u0001\b\u0015\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0001\u0000\u0001\u0000\u0001\u0000\u0005\u0000\n\b"+
		"\u0000\n\u0000\f\u0000\r\t\u0000\u0001\u0001\u0001\u0001\u0001\u0001\u0001"+
		"\u0001\u0001\u0002\u0001\u0002\u0001\u0002\u0000\u0000\u0003\u0000\u0002"+
		"\u0004\u0000\u0000\u0014\u0000\u000b\u0001\u0000\u0000\u0000\u0002\u000e"+
		"\u0001\u0000\u0000\u0000\u0004\u0012\u0001\u0000\u0000\u0000\u0006\n\u0003"+
		"\u0002\u0001\u0000\u0007\n\u0005\u0004\u0000\u0000\b\n\u0005\u0003\u0000"+
		"\u0000\t\u0006\u0001\u0000\u0000\u0000\t\u0007\u0001\u0000\u0000\u0000"+
		"\t\b\u0001\u0000\u0000\u0000\n\r\u0001\u0000\u0000\u0000\u000b\t\u0001"+
		"\u0000\u0000\u0000\u000b\f\u0001\u0000\u0000\u0000\f\u0001\u0001\u0000"+
		"\u0000\u0000\r\u000b\u0001\u0000\u0000\u0000\u000e\u000f\u0005\u0001\u0000"+
		"\u0000\u000f\u0010\u0005\u0007\u0000\u0000\u0010\u0011\u0003\u0004\u0002"+
		"\u0000\u0011\u0003\u0001\u0000\u0000\u0000\u0012\u0013\u0005\u0002\u0000"+
		"\u0000\u0013\u0005\u0001\u0000\u0000\u0000\u0002\t\u000b";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}