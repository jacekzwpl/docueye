grammar LinterExpressions;

/*
 * Parser Rules
 */
elementExpression: (elementTagExpr)*;


elementTagExpr          : ELEMENT_TAG ( EQUALS | NOTEQUALS ) expressionValue+ 
                        | ELEMENT_TAG ( IN | NOTIN)'[' expressionValue (',' expressionValue)* ']'
                        ;

expressionValue: TEXT | NULL ;
/*
 * Lexer Rules
 */
fragment A          : ('A'|'a') ;
fragment B          : ('B'|'b') ;
fragment C          : ('C'|'c') ;
fragment D          : ('D'|'d') ;
fragment E          : ('E'|'e') ;
fragment F          : ('F'|'f') ;
fragment G          : ('G'|'g') ;
fragment H          : ('H'|'h') ;
fragment I          : ('I'|'i') ;
fragment J          : ('J'|'j') ;
fragment K          : ('K'|'k') ;
fragment L          : ('L'|'l') ;
fragment M          : ('M'|'m') ;
fragment N          : ('N'|'n') ;
fragment O          : ('O'|'o') ;
fragment P          : ('P'|'p') ;
fragment Q          : ('Q'|'q') ;
fragment R          : ('R'|'r') ;
fragment S          : ('S'|'s') ;
fragment T          : ('T'|'t') ;
fragment U          : ('U'|'u') ;
fragment V          : ('V'|'v') ;
fragment W          : ('W'|'w') ;
fragment X          : ('X'|'x') ;
fragment Y          : ('Y'|'y') ;
fragment Z          : ('Z'|'z') ;

fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;
fragment NUMBER     : [0-9] ;


ELEMENT             : E L E M E N T ;
ELEMENT_TAG         : E L E M E N T DOT T A G ;  

TEXT                : '"' .*? '"' ;
DOT                 : '.' ;
EQUALS              : '==' ;
NOTEQUALS           : '!=' ;
IN                  : 'in' ;
NOTIN               : 'notin' ;
NULL                : 'null' ;


WHITESPACE          : (' '|'\t')+ -> skip ;