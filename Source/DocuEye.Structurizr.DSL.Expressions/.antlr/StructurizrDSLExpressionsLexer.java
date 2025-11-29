// Generated from c:/nCode/Parser/DocuEye.Structurizr.DSL.Expressions/StructurizrDSLExpressions.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue", "this-escape"})
public class StructurizrDSLExpressionsLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		ELEMENT_TYPE=1, VALUE=2, OR=3, AND=4, WILDCARD=5, DOT=6, EQUALS=7, WHITESPACE=8;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", 
			"O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "LOWERCASE", 
			"UPPERCASE", "NUMBER", "ELEMENT_TYPE", "VALUE", "OR", "AND", "WILDCARD", 
			"DOT", "EQUALS", "WHITESPACE"
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


	public StructurizrDSLExpressionsLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "StructurizrDSLExpressions.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\u0004\u0000\b\u00ac\u0006\uffff\uffff\u0002\u0000\u0007\u0000\u0002\u0001"+
		"\u0007\u0001\u0002\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004"+
		"\u0007\u0004\u0002\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007"+
		"\u0007\u0007\u0002\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b"+
		"\u0007\u000b\u0002\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002"+
		"\u000f\u0007\u000f\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011\u0002"+
		"\u0012\u0007\u0012\u0002\u0013\u0007\u0013\u0002\u0014\u0007\u0014\u0002"+
		"\u0015\u0007\u0015\u0002\u0016\u0007\u0016\u0002\u0017\u0007\u0017\u0002"+
		"\u0018\u0007\u0018\u0002\u0019\u0007\u0019\u0002\u001a\u0007\u001a\u0002"+
		"\u001b\u0007\u001b\u0002\u001c\u0007\u001c\u0002\u001d\u0007\u001d\u0002"+
		"\u001e\u0007\u001e\u0002\u001f\u0007\u001f\u0002 \u0007 \u0002!\u0007"+
		"!\u0002\"\u0007\"\u0002#\u0007#\u0002$\u0007$\u0001\u0000\u0001\u0000"+
		"\u0001\u0001\u0001\u0001\u0001\u0002\u0001\u0002\u0001\u0003\u0001\u0003"+
		"\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005\u0001\u0006\u0001\u0006"+
		"\u0001\u0007\u0001\u0007\u0001\b\u0001\b\u0001\t\u0001\t\u0001\n\u0001"+
		"\n\u0001\u000b\u0001\u000b\u0001\f\u0001\f\u0001\r\u0001\r\u0001\u000e"+
		"\u0001\u000e\u0001\u000f\u0001\u000f\u0001\u0010\u0001\u0010\u0001\u0011"+
		"\u0001\u0011\u0001\u0012\u0001\u0012\u0001\u0013\u0001\u0013\u0001\u0014"+
		"\u0001\u0014\u0001\u0015\u0001\u0015\u0001\u0016\u0001\u0016\u0001\u0017"+
		"\u0001\u0017\u0001\u0018\u0001\u0018\u0001\u0019\u0001\u0019\u0001\u001a"+
		"\u0001\u001a\u0001\u001b\u0001\u001b\u0001\u001c\u0001\u001c\u0001\u001d"+
		"\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d"+
		"\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d"+
		"\u0001\u001e\u0001\u001e\u0004\u001e\u0095\b\u001e\u000b\u001e\f\u001e"+
		"\u0096\u0001\u001f\u0001\u001f\u0001\u001f\u0001 \u0001 \u0001 \u0001"+
		"!\u0001!\u0001\"\u0001\"\u0001#\u0001#\u0001#\u0001$\u0004$\u00a7\b$\u000b"+
		"$\f$\u00a8\u0001$\u0001$\u0000\u0000%\u0001\u0000\u0003\u0000\u0005\u0000"+
		"\u0007\u0000\t\u0000\u000b\u0000\r\u0000\u000f\u0000\u0011\u0000\u0013"+
		"\u0000\u0015\u0000\u0017\u0000\u0019\u0000\u001b\u0000\u001d\u0000\u001f"+
		"\u0000!\u0000#\u0000%\u0000\'\u0000)\u0000+\u0000-\u0000/\u00001\u0000"+
		"3\u00005\u00007\u00009\u0000;\u0001=\u0002?\u0003A\u0004C\u0005E\u0006"+
		"G\u0007I\b\u0001\u0000\u001e\u0002\u0000AAaa\u0002\u0000BBbb\u0002\u0000"+
		"CCcc\u0002\u0000DDdd\u0002\u0000EEee\u0002\u0000FFff\u0002\u0000GGgg\u0002"+
		"\u0000HHhh\u0002\u0000IIii\u0002\u0000JJjj\u0002\u0000KKkk\u0002\u0000"+
		"LLll\u0002\u0000MMmm\u0002\u0000NNnn\u0002\u0000OOoo\u0002\u0000PPpp\u0002"+
		"\u0000QQqq\u0002\u0000RRrr\u0002\u0000SSss\u0002\u0000TTtt\u0002\u0000"+
		"UUuu\u0002\u0000VVvv\u0002\u0000WWww\u0002\u0000XXxx\u0002\u0000YYyy\u0002"+
		"\u0000ZZzz\u0001\u0000az\u0001\u0000AZ\u0001\u000009\u0002\u0000\t\t "+
		" \u0091\u0000;\u0001\u0000\u0000\u0000\u0000=\u0001\u0000\u0000\u0000"+
		"\u0000?\u0001\u0000\u0000\u0000\u0000A\u0001\u0000\u0000\u0000\u0000C"+
		"\u0001\u0000\u0000\u0000\u0000E\u0001\u0000\u0000\u0000\u0000G\u0001\u0000"+
		"\u0000\u0000\u0000I\u0001\u0000\u0000\u0000\u0001K\u0001\u0000\u0000\u0000"+
		"\u0003M\u0001\u0000\u0000\u0000\u0005O\u0001\u0000\u0000\u0000\u0007Q"+
		"\u0001\u0000\u0000\u0000\tS\u0001\u0000\u0000\u0000\u000bU\u0001\u0000"+
		"\u0000\u0000\rW\u0001\u0000\u0000\u0000\u000fY\u0001\u0000\u0000\u0000"+
		"\u0011[\u0001\u0000\u0000\u0000\u0013]\u0001\u0000\u0000\u0000\u0015_"+
		"\u0001\u0000\u0000\u0000\u0017a\u0001\u0000\u0000\u0000\u0019c\u0001\u0000"+
		"\u0000\u0000\u001be\u0001\u0000\u0000\u0000\u001dg\u0001\u0000\u0000\u0000"+
		"\u001fi\u0001\u0000\u0000\u0000!k\u0001\u0000\u0000\u0000#m\u0001\u0000"+
		"\u0000\u0000%o\u0001\u0000\u0000\u0000\'q\u0001\u0000\u0000\u0000)s\u0001"+
		"\u0000\u0000\u0000+u\u0001\u0000\u0000\u0000-w\u0001\u0000\u0000\u0000"+
		"/y\u0001\u0000\u0000\u00001{\u0001\u0000\u0000\u00003}\u0001\u0000\u0000"+
		"\u00005\u007f\u0001\u0000\u0000\u00007\u0081\u0001\u0000\u0000\u00009"+
		"\u0083\u0001\u0000\u0000\u0000;\u0085\u0001\u0000\u0000\u0000=\u0094\u0001"+
		"\u0000\u0000\u0000?\u0098\u0001\u0000\u0000\u0000A\u009b\u0001\u0000\u0000"+
		"\u0000C\u009e\u0001\u0000\u0000\u0000E\u00a0\u0001\u0000\u0000\u0000G"+
		"\u00a2\u0001\u0000\u0000\u0000I\u00a6\u0001\u0000\u0000\u0000KL\u0007"+
		"\u0000\u0000\u0000L\u0002\u0001\u0000\u0000\u0000MN\u0007\u0001\u0000"+
		"\u0000N\u0004\u0001\u0000\u0000\u0000OP\u0007\u0002\u0000\u0000P\u0006"+
		"\u0001\u0000\u0000\u0000QR\u0007\u0003\u0000\u0000R\b\u0001\u0000\u0000"+
		"\u0000ST\u0007\u0004\u0000\u0000T\n\u0001\u0000\u0000\u0000UV\u0007\u0005"+
		"\u0000\u0000V\f\u0001\u0000\u0000\u0000WX\u0007\u0006\u0000\u0000X\u000e"+
		"\u0001\u0000\u0000\u0000YZ\u0007\u0007\u0000\u0000Z\u0010\u0001\u0000"+
		"\u0000\u0000[\\\u0007\b\u0000\u0000\\\u0012\u0001\u0000\u0000\u0000]^"+
		"\u0007\t\u0000\u0000^\u0014\u0001\u0000\u0000\u0000_`\u0007\n\u0000\u0000"+
		"`\u0016\u0001\u0000\u0000\u0000ab\u0007\u000b\u0000\u0000b\u0018\u0001"+
		"\u0000\u0000\u0000cd\u0007\f\u0000\u0000d\u001a\u0001\u0000\u0000\u0000"+
		"ef\u0007\r\u0000\u0000f\u001c\u0001\u0000\u0000\u0000gh\u0007\u000e\u0000"+
		"\u0000h\u001e\u0001\u0000\u0000\u0000ij\u0007\u000f\u0000\u0000j \u0001"+
		"\u0000\u0000\u0000kl\u0007\u0010\u0000\u0000l\"\u0001\u0000\u0000\u0000"+
		"mn\u0007\u0011\u0000\u0000n$\u0001\u0000\u0000\u0000op\u0007\u0012\u0000"+
		"\u0000p&\u0001\u0000\u0000\u0000qr\u0007\u0013\u0000\u0000r(\u0001\u0000"+
		"\u0000\u0000st\u0007\u0014\u0000\u0000t*\u0001\u0000\u0000\u0000uv\u0007"+
		"\u0015\u0000\u0000v,\u0001\u0000\u0000\u0000wx\u0007\u0016\u0000\u0000"+
		"x.\u0001\u0000\u0000\u0000yz\u0007\u0017\u0000\u0000z0\u0001\u0000\u0000"+
		"\u0000{|\u0007\u0018\u0000\u0000|2\u0001\u0000\u0000\u0000}~\u0007\u0019"+
		"\u0000\u0000~4\u0001\u0000\u0000\u0000\u007f\u0080\u0007\u001a\u0000\u0000"+
		"\u00806\u0001\u0000\u0000\u0000\u0081\u0082\u0007\u001b\u0000\u0000\u0082"+
		"8\u0001\u0000\u0000\u0000\u0083\u0084\u0007\u001c\u0000\u0000\u0084:\u0001"+
		"\u0000\u0000\u0000\u0085\u0086\u0003\t\u0004\u0000\u0086\u0087\u0003\u0017"+
		"\u000b\u0000\u0087\u0088\u0003\t\u0004\u0000\u0088\u0089\u0003\u0019\f"+
		"\u0000\u0089\u008a\u0003\t\u0004\u0000\u008a\u008b\u0003\u001b\r\u0000"+
		"\u008b\u008c\u0003\'\u0013\u0000\u008c\u008d\u0003E\"\u0000\u008d\u008e"+
		"\u0003\'\u0013\u0000\u008e\u008f\u00031\u0018\u0000\u008f\u0090\u0003"+
		"\u001f\u000f\u0000\u0090\u0091\u0003\t\u0004\u0000\u0091<\u0001\u0000"+
		"\u0000\u0000\u0092\u0095\u00035\u001a\u0000\u0093\u0095\u00037\u001b\u0000"+
		"\u0094\u0092\u0001\u0000\u0000\u0000\u0094\u0093\u0001\u0000\u0000\u0000"+
		"\u0095\u0096\u0001\u0000\u0000\u0000\u0096\u0094\u0001\u0000\u0000\u0000"+
		"\u0096\u0097\u0001\u0000\u0000\u0000\u0097>\u0001\u0000\u0000\u0000\u0098"+
		"\u0099\u0005|\u0000\u0000\u0099\u009a\u0005|\u0000\u0000\u009a@\u0001"+
		"\u0000\u0000\u0000\u009b\u009c\u0005&\u0000\u0000\u009c\u009d\u0005&\u0000"+
		"\u0000\u009dB\u0001\u0000\u0000\u0000\u009e\u009f\u0005*\u0000\u0000\u009f"+
		"D\u0001\u0000\u0000\u0000\u00a0\u00a1\u0005.\u0000\u0000\u00a1F\u0001"+
		"\u0000\u0000\u0000\u00a2\u00a3\u0005=\u0000\u0000\u00a3\u00a4\u0005=\u0000"+
		"\u0000\u00a4H\u0001\u0000\u0000\u0000\u00a5\u00a7\u0007\u001d\u0000\u0000"+
		"\u00a6\u00a5\u0001\u0000\u0000\u0000\u00a7\u00a8\u0001\u0000\u0000\u0000"+
		"\u00a8\u00a6\u0001\u0000\u0000\u0000\u00a8\u00a9\u0001\u0000\u0000\u0000"+
		"\u00a9\u00aa\u0001\u0000\u0000\u0000\u00aa\u00ab\u0006$\u0000\u0000\u00ab"+
		"J\u0001\u0000\u0000\u0000\u0004\u0000\u0094\u0096\u00a8\u0001\u0006\u0000"+
		"\u0000";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}