grammar StructurizrDSLExpressions;

/*
 * Parser Rules
 */
generalExpression: (
    elementTypeExpr
    | elementParentExpr
    | elementTagExpr
    | elementTechnologyExpr
    | elementPropertiesExpr
    | elementWithCouplingsExpr
    | relationshipTagExpr
    | relationshipPropertiesExpr
    | relationshipSourceExpr
    | relationshipDestinationExpr
    | relationshipSpecifiedExpr
    | relationshipAllExpr
    | expressionConnector)* ;

elementTypeExpr         : ELEMENT_TYPE EQUALS expressionValue ;
elementParentExpr       : ELEMENT_PARENT EQUALS expressionValue ;
elementTagExpr          : ELEMENT_TAG ( EQUALS | NOTEQUALS ) expressionValue+ ;
elementTechnologyExpr   : ELEMENT_TECHNOLOGY ( EQUALS | NOTEQUALS ) expressionValue ;
elementPropertiesExpr   : ELEMENT_PROPERTIES '[' propertyName ']' ( EQUALS | NOTEQUALS ) expressionValue ;

elementWithCouplingsExpr    : ELEMENT EQUALS relationshipPointer? expressionValue relationshipPointer? MORE_THAN?
                            | relationshipPointer? expressionValue relationshipPointer? MORE_THAN?
                            ;

relationshipTagExpr         : RELATIONSHIP_TAG ( EQUALS | NOTEQUALS ) expressionValue+ ;
relationshipPropertiesExpr  : RELATIONSHIP_PROPERTIES '[' propertyName ']' ( EQUALS | NOTEQUALS ) expressionValue ;
relationshipSourceExpr      : RELATIONSHIP_SOURCE EQUALS expressionValue 
                            | RELATIONSHIP EQUALS expressionValue RELATIONSHIP_POINTER? MORE_THAN? '*'
                            | expressionValue RELATIONSHIP_POINTER? MORE_THAN? '*'
                            ;        
relationshipDestinationExpr : RELATIONSHIP_DESTINATION EQUALS expressionValue 
                            | '*' RELATIONSHIP_POINTER expressionValue
                            | RELATIONSHIP EQUALS '*' RELATIONSHIP_POINTER expressionValue
                            ;
relationshipSpecifiedExpr   : RELATIONSHIP EQUALS sourceElement RELATIONSHIP_POINTER? MORE_THAN? destinationElement ;

relationshipAllExpr         : '*' RELATIONSHIP_POINTER '*'
                            | RELATIONSHIP EQUALS '*'
                            | RELATIONSHIP EQUALS '*' RELATIONSHIP_POINTER '*' 
                            ;


sourceElement : VALUE ;
destinationElement : VALUE ;
relationshipPointer : RELATIONSHIP_POINTER;
expressionValue: VALUE ;
propertyName: VALUE ;
expressionConnector : (AND | OR) ;

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
ELEMENT_TYPE        : E L E M E N T DOT T Y P E ;
ELEMENT_PARENT      : E L E M E N T DOT P A R E N T ;
ELEMENT_TAG         : E L E M E N T DOT T A G ;  
ELEMENT_TECHNOLOGY  : E L E M E N T DOT T E C H N O L O G Y ;
ELEMENT_PROPERTIES  : E L E M E N T DOT P R O P E R T I E S ;

RELATIONSHIP                : R E L A T I O N S H I P ;
RELATIONSHIP_TAG            : R E L A T I O N S H I P DOT T A G ;  
RELATIONSHIP_PROPERTIES     : R E L A T I O N S H I P DOT P R O P E R T I E S ;
RELATIONSHIP_SOURCE         : R E L A T I O N S H I P DOT S O U R C E ;
RELATIONSHIP_DESTINATION    : R E L A T I O N S H I P DOT D E S T I N A T I O N ;

VALUE               : (LOWERCASE | UPPERCASE | NUMBER | '-' | '.' | '_' | ',')+ ;
OR                  : '||';
AND                 : '&&';
WILDCARD            : '*' ;
DOT                 : '.' ;
EQUALS              : '==' ;
NOTEQUALS           : '!=' ;
RELATIONSHIP_POINTER: '->' ;
MORE_THAN          : '>' ;

WHITESPACE          : (' '|'\t')+ -> skip ;

//VALUE : .*? ('&' | '|' | EOF)  ;

// COMMENT : .*? ('&&' | '||' ) ; // .*? matches anything until the first */
//ELEMENT_VALUE       : ( 
//                        ~('&' | '|')        // Match any character except '&' or '|'
//                        | ('&' ~'&')        // Match a single '&' not followed by another '&'
 //                       | ('|' ~'|')        // Match a single '|' not followed by another '|'
//                    )+ ;                    // Match one or more of the above


