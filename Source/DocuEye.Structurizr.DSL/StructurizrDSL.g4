grammar StructurizrDSL;

/*
 * Parser Rules
 */
workspace           : NEWLINE? WORKSPACE name? description? BEGIN workspaceBody END NEWLINE? EOF;
workspaceBody       : ( 
                        nameBlock 
                        | descriptionBlock 
                        | identifiersKeyword 
                        | docsKeyword
                        | adrsKeyword
                        | properties
                        | model 
                        | views
                        | configuration
                        | includeFile
                        | WHITESPACE | NEWLINE 
                      )* ;

identifiersKeyword  : IDENTIFIERSKEY identifiersValue NEWLINE;
identifiersValue    : WORD ; 

docsKeyword         : DOCS docsValue NEWLINE;
docsValue           : TEXT | PATH | WORD ;

adrsKeyword         : ADRS adrsValue NEWLINE;
adrsValue           : TEXT | PATH | WORD ;

model               : MODEL BEGIN modelBody END ;
modelBody           : (
                        identifiersKeyword 
                        | modelGroup
                        | person 
                        | softwareSystem
                        | element
                        | relationship
                        | properties
                        | deploymentEnvironment
                        | includeFile
                        | modelElementReference
                        | WHITESPACE | NEWLINE
                      )* ;

modelGroup          : GROUP name NEWLINE? BEGIN modelGroupBody END NEWLINE ;
modelGroupBody      : (
                        person
                        | softwareSystem
                        | element
                        | modelGroup
                        | deploymentEnvironment
                        | includeFile
                        | relationship
                        | WHITESPACE | NEWLINE)* ;
 
person              : identifier '=' PERSON name description? tags? NEWLINE
                    | identifier '=' PERSON name description? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | PERSON name description? tags? NEWLINE
                    | PERSON name description? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ;

element             : identifier '=' ELEMENT name metadata? description? tags? NEWLINE
                    | identifier '=' ELEMENT name metadata? description? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | ELEMENT name metadata? description? tags? NEWLINE
                    | ELEMENT name metadata? description? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ;

softwareSystem      : identifier '=' SOFTWARESYSTEM name description? tags? NEWLINE
                    | identifier '=' SOFTWARESYSTEM name description? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | SOFTWARESYSTEM name description? tags? NEWLINE
                    | SOFTWARESYSTEM name description? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ;

container           : identifier '=' CONTAINER name description? technology? tags? NEWLINE
                    | identifier '=' CONTAINER name description? technology? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | CONTAINER name description? technology? tags? NEWLINE
                    | CONTAINER name description? technology? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ; 

component           : identifier '=' COMPONENT name description? technology? tags? NEWLINE
                    | identifier '=' COMPONENT name description? technology? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | COMPONENT name description? technology? tags? NEWLINE
                    | COMPONENT name description? technology? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ;

deploymentEnvironment : identifier '=' DEPLOYMENTENVIRONMENT name NEWLINE
                      | identifier '=' DEPLOYMENTENVIRONMENT name NEWLINE? BEGIN modelElementBody END NEWLINE
                      | DEPLOYMENTENVIRONMENT name NEWLINE
                      | DEPLOYMENTENVIRONMENT name NEWLINE? BEGIN modelElementBody END NEWLINE
                      ;

deploymentNode      : identifier '=' DEPLOYMENTNODE name description? technology? tags? instances? NEWLINE
                    | identifier '=' DEPLOYMENTNODE name description? technology? tags? instances? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | DEPLOYMENTNODE name description? technology? tags? instances? NEWLINE
                    | DEPLOYMENTNODE name description? technology? tags? instances? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ;


infrastructureNode  : identifier '=' INFRASTRUCTURENODE name description? technology? tags? NEWLINE
                    | identifier '=' INFRASTRUCTURENODE name description? technology? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | INFRASTRUCTURENODE name description? technology? tags? NEWLINE
                    | INFRASTRUCTURENODE name description? technology? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ;

softwareSystemInstance : identifier '=' SOFTWARESYSTEMINSTANCE identifierReference deploymentGroupsRef? tags? NEWLINE
                    | identifier '=' SOFTWARESYSTEMINSTANCE identifierReference deploymentGroupsRef? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | SOFTWARESYSTEMINSTANCE identifierReference deploymentGroupsRef? tags? NEWLINE
                    | SOFTWARESYSTEMINSTANCE identifierReference deploymentGroupsRef? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ;

containerInstance : identifier '=' CONTAINERINSTANCE identifierReference deploymentGroupsRef? tags? NEWLINE
                    | identifier '=' CONTAINERINSTANCE identifierReference deploymentGroupsRef? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | CONTAINERINSTANCE identifierReference deploymentGroupsRef? tags? NEWLINE
                    | CONTAINERINSTANCE identifierReference deploymentGroupsRef? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ;

deploymentGroup     : identifier '=' DEPLOYMENTGROUP name NEWLINE
                    | DEPLOYMENTGROUP name NEWLINE
                    ;

bulkModelElements       : EXELEMENTS expressionValue NEWLINE? BEGIN modelElementBody END NEWLINE ;
bulkModelRelationships  : EXRELATIONSHIPS expressionValue NEWLINE? BEGIN modelElementBody END NEWLINE ;
  
modelElementBody    : (
                      tagsBlock 
                      | descriptionBlock 
                      | properties 
                      | urlBlock 
                      | relationship 
                      | perspectives
                      | docsKeyword
                      | adrsKeyword
                      | container
                      | modelElementGroup
                      | includeFile
                      | technologyBlock
                      | component
                      | deploymentNode
                      | deploymentGroup
                      | instancesBlock
                      | infrastructureNode
                      | softwareSystemInstance
                      | containerInstance
                      | healthCheckBlock
                      | WHITESPACE | NEWLINE)* ;
modelElementGroup : GROUP name NEWLINE? BEGIN modelElementGroupBody END NEWLINE ;
modelElementGroupBody : (
                            container
                            | component
                            | deploymentNode
                            | deploymentGroup
                            | modelElementGroup
                            | includeFile
                            | WHITESPACE | NEWLINE)* ;

views               : VIEWS BEGIN (
                      systemLandScape
                      | systemContext
                      | containerView
                      | componentView
                      | filteredView
                      | deploymentView
                      | customView
                      | imageView
                      | dynamicView
                      | styles
                      | branding
                      | terminology
                      | properties
                      | themeBlock
                      | themesBlock
                      | includeFile
                      | WHITESPACE | NEWLINE)* END ;


systemLandScape     : SYSTEMLANDSCAPE key? description? NEWLINE? BEGIN systemLandScapeBody END ;
systemLandScapeBody : (
                      descriptionBlock
                      | properties
                      | includeBlock
                      | excludeBlock
                      | autoLayoutBlock
                      | defaultBlock
                      | animation
                      | titleBlock
                      | WHITESPACE | NEWLINE)* ;

systemContext     : SYSTEMCONTEXT identifierReference key? description? NEWLINE? BEGIN systemContextBody END ;
systemContextBody : (
                      descriptionBlock
                      | properties
                      | includeBlock
                      | excludeBlock
                      | autoLayoutBlock
                      | defaultBlock
                      | animation
                      | titleBlock
                      | WHITESPACE | NEWLINE)* ;

containerView     : CONTAINER identifierReference key? description? NEWLINE? BEGIN containerViewBody END ;
containerViewBody : (
                      descriptionBlock
                      | properties
                      | includeBlock
                      | excludeBlock
                      | autoLayoutBlock
                      | defaultBlock
                      | animation
                      | titleBlock
                      | WHITESPACE | NEWLINE)* ;

componentView     : COMPONENT identifierReference key? description? NEWLINE? BEGIN componentViewBody END ;
componentViewBody : (
                      descriptionBlock
                      | properties
                      | includeBlock
                      | excludeBlock
                      | autoLayoutBlock
                      | defaultBlock
                      | animation
                      | titleBlock
                      | WHITESPACE | NEWLINE)* ;

filteredView     : FILTERED name filteredViewMode tags key? description? NEWLINE? BEGIN filteredViewBody END ;
filteredViewBody : (
                      descriptionBlock
                      | properties
                      | defaultBlock
                      | titleBlock
                      | WHITESPACE | NEWLINE)* ;

deploymentView     : DEPLOYMENT deploymentViewContext environmentReference key? description? NEWLINE? BEGIN deploymentViewBody END ;
deploymentViewBody : (
                      descriptionBlock
                      | properties
                      | includeBlock
                      | excludeBlock
                      | autoLayoutBlock
                      | defaultBlock
                      | animation
                      | titleBlock
                      | WHITESPACE | NEWLINE)* ;

customView     : CUSTOM key? title? description? NEWLINE? BEGIN customViewBody END ;
customViewBody : (
                      descriptionBlock
                      | properties
                      | includeBlock
                      | excludeBlock
                      | autoLayoutBlock
                      | defaultBlock
                      | animation
                      | titleBlock
                      | WHITESPACE | NEWLINE)* ;

imageView     : IMAGE imageViewContext key? NEWLINE? BEGIN imageViewBody END ;
imageViewBody : (
                      descriptionBlock
                      | properties
                      | defaultBlock
                      | titleBlock
                      | imageBlock
                      | plantumlBlock
                      | mermaidBlock
                      | krokiBlock
                      | WHITESPACE | NEWLINE)* ;

dynamicView     : DYNAMIC dynamicViewContext key? description? NEWLINE? BEGIN dynamicViewBody END ;
dynamicViewBody : (
                      descriptionBlock
                      | properties
                      | autoLayoutBlock
                      | defaultBlock
                      | titleBlock
                      | dynamicViewRelationship
                      | dynamicViewRelationshipGroup
                      | WHITESPACE | NEWLINE)* ;

dynamicViewRelationship : dynamicViewRelationshipOrder relationshipSource RELATIONSHIP relationshipTarget description? technology? NEWLINE
                      | relationshipSource RELATIONSHIP relationshipTarget description? technology? NEWLINE
                      | dynamicViewRelationshipOrder identifierReference description? NEWLINE
                      | identifierReference description? NEWLINE ;
dynamicViewRelationshipOrder : WORD ;
dynamicViewRelationshipGroup : BEGIN dynamicViewRelationshipGroupBody END NEWLINE ;
dynamicViewRelationshipGroupBody : (
                      dynamicViewRelationship
                      | dynamicViewRelationshipGroup
                      | WHITESPACE | NEWLINE)* ;

styles              : STYLES BEGIN (
                      elementStyle
                      | relationshipStyle
                      | WHITESPACE | NEWLINE)* END ;

elementStyle        : ELEMENT tag NEWLINE? BEGIN elementStyleBody END NEWLINE ;
elementStyleBody    : (
                      styleShapeBlock
                      | styleIconBlock
                      | styleWidthBlock
                      | styleHeightBlock
                      | styleColorBlock
                      | styleStrokeBlock
                      | styleStrokeWidthBlock
                      | styleFontSizeBlock
                      | styleOpacityBlock
                      | styleMetadataBlock
                      | styleDescriptionBlock
                      | styleBackgroundBlock
                      | styleBorderBlock
                      | properties
                      | WHITESPACE | NEWLINE)* ;

relationshipStyle    : WRELATIONSHIP tag NEWLINE? BEGIN relationshipStyleBody END NEWLINE ;
relationshipStyleBody    : (
                      styleColorBlock
                      | styleFontSizeBlock
                      | styleWidthBlock
                      | styleOpacityBlock
                      | styleThicknessBlock
                      | styleStyleBlock
                      | styleRoutingBlock
                      | stylePositionBlock
                      | properties
                      | WHITESPACE | NEWLINE)* ;

styleShapeBlock     : SHAPE value NEWLINE ;
styleIconBlock      : ICON imageValue NEWLINE ;
styleWidthBlock     : WIDTH value NEWLINE ;
styleHeightBlock    : HEIGHT value NEWLINE ;
styleBackgroundBlock : BACKGROUND value NEWLINE ;
styleColorBlock     : COLOR value NEWLINE ;
styleStrokeBlock    : STROKE value NEWLINE ;
styleStrokeWidthBlock : STROKEWIDTH value NEWLINE ;
styleFontSizeBlock  : FONTSIZE value NEWLINE ;
styleOpacityBlock   : OPACITY value NEWLINE ;
styleMetadataBlock  : METADATA value NEWLINE ;
styleDescriptionBlock : DESCRIPTION value NEWLINE ;
styleBorderBlock    : BORDER value NEWLINE ;
styleThicknessBlock : THICKNESS value NEWLINE ;
styleStyleBlock     : STYLE value NEWLINE ;
styleRoutingBlock   : ROUTING value NEWLINE ;
stylePositionBlock  : POSITION value NEWLINE ;

branding            : BRANDING BEGIN (
                      brandingLogoBlock
                      | brandingFontBlock
                      | WHITESPACE | NEWLINE)* END ;

brandingLogoBlock   : LOGO imageValue NEWLINE ;
brandingFontBlock   : FONT value NEWLINE ;

terminology         : TERMINOLOGY BEGIN (
                      terminologyPersonBlock
                      | terminologySoftwareSystemBlock
                      | terminologyContainerBlock
                      | terminologyComponentBlock
                      | terminologyDeploymentNodeBlock
                      | terminologyInfrastructureNodeBlock
                      | terminologyRelationshipBlock
                      | WHITESPACE | NEWLINE)* END ;

terminologyPersonBlock : PERSON value NEWLINE ;
terminologySoftwareSystemBlock : SOFTWARESYSTEM value NEWLINE ;
terminologyContainerBlock : CONTAINER value NEWLINE ;
terminologyComponentBlock : COMPONENT value NEWLINE ;
terminologyDeploymentNodeBlock : DEPLOYMENTNODE value NEWLINE ;
terminologyInfrastructureNodeBlock : INFRASTRUCTURENODE value NEWLINE ;
terminologyRelationshipBlock : WRELATIONSHIP value NEWLINE ; 

animation           : ANIMATION NEWLINE? BEGIN animationBody END ;
animationBody       : (
                      animationStep
                      | WHITESPACE | NEWLINE)* ;

properties          : PROPERTIES BEGIN (property | WHITESPACE | NEWLINE)* END;
property            : name propertyValue NEWLINE ;
propertyValue       : TEXT | WORD ;

tagsBlock           : TAGS (tags)* NEWLINE;
descriptionBlock    : DESCRIPTION description NEWLINE;
nameBlock           : NAME name NEWLINE;
urlBlock            : URL url NEWLINE;
technologyBlock     : TECHNOLOGY technology NEWLINE;
instancesBlock      : INSTANCES instances NEWLINE;
healthCheckBlock    : HEALTHCHECK name url interval? timeout? NEWLINE;
deploymentGroupsRef : TEXT ;
includeBlock        : INCLUDE (includeValue)* NEWLINE ;
includeValue        : WILDCARD | WORD | TEXT ;
excludeBlock        : EXCLUDE (excludeValue)* NEWLINE ;
excludeValue        : WILDCARD | WORD | TEXT ;
autoLayoutBlock     : AUTOLAYOUT rankDirection? rankSeparation? nodeSeparation? NEWLINE ;
defaultBlock        : DEFAULT NEWLINE ;
animationStep       : (identifier)+ NEWLINE ;
titleBlock          : TITLE value NEWLINE ;
filteredViewMode    : INCLUDE | EXCLUDE ;
deploymentViewContext : WORD | WILDCARD ;
imageViewContext      : WORD | WILDCARD ;
plantumlBlock         : PLANTUML plantumlValue NEWLINE ;
plantumlValue         : TEXT | PATH | WORD | URLVALUE ;
mermaidBlock          : MERMAID mermaidValue NEWLINE ;
mermaidValue          : TEXT | PATH | WORD | URLVALUE ;
krokiBlock            : KROKI krokiFormat krokiValue NEWLINE ;
krokiFormat           : PLANTUML | MERMAID | WORD | TEXT ;
krokiValue            : TEXT | PATH | WORD | URLVALUE ;
imageBlock            : IMAGE imageValue NEWLINE ;
imageValue            : TEXT | PATH | WORD | URLVALUE ;
dynamicViewContext     : WORD | WILDCARD ;
environmentReference : TEXT | WORD ;
expressionValue     : WILDCARD | WORD | TEXT ;




name                : TEXT | WORD;
value               : TEXT | WORD;
metadata            : TEXT | WORD;
description         : TEXT ;
identifierReference : WORD ;
identifier          : WORD ;
key                 : TEXT | WORD ;
tags                : TEXT ;    
url                 : URLVALUE ;
technology          : TEXT | WORD;
instances           : TEXT | WORD;
interval            : TEXT | WORD ;
timeout             : TEXT | WORD ;
rankDirection       : TEXT | WORD ;
rankSeparation      : TEXT | WORD ;
nodeSeparation      : TEXT | WORD ;
title               : TEXT | WORD ;
tag                 : TEXT | WORD ;

relationship        : identifier '=' relationshipSource? RELATIONSHIP relationshipTarget description? technology? tags? NEWLINE
                    | identifier '=' relationshipSource? RELATIONSHIP relationshipTarget description? technology? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    | relationshipSource? RELATIONSHIP relationshipTarget description? technology? tags? NEWLINE
                    | relationshipSource? RELATIONSHIP relationshipTarget description? technology? tags? NEWLINE? BEGIN modelElementBody END NEWLINE
                    ;

relationshipSource  : identifier ;
relationshipTarget  : identifier ;

perspectives        : PERSPECTIVES BEGIN (perspective | WHITESPACE | NEWLINE)* END ;
perspective         : name description value? NEWLINE ;

themeBlock          : THEME value NEWLINE ;
themesBlock         : THEMES (themesValue)* NEWLINE ;
themesValue         : WORD | PATH | URLVALUE | TEXT;

configuration       : CONFIGURATION BEGIN (
                    configurationScopeBlock 
                    | configurationVisibilityBlock
                    | configurationUsers
                    | properties
                    | WHITESPACE 
                    | NEWLINE)* END ;

configurationScopeBlock : SCOPE value NEWLINE ;
configurationVisibilityBlock : VISIBILITY value NEWLINE ;
configurationUsers : USERS BEGIN (
                    configurationUser
                    | WHITESPACE 
                    | NEWLINE)* END ;

configurationUser : username userrole NEWLINE ;
username : TEXT | WORD ;
userrole : TEXT | WORD ;

includeFile : EXINCLIDE includeFileValue NEWLINE ;
includeFileValue : TEXT | PATH | WORD | URLVALUE;

modelElementReference : EXREF identifierReference NEWLINE? BEGIN modelElementBody END NEWLINE 
                      | EXELEMENT identifierReference NEWLINE? BEGIN modelElementBody END NEWLINE 
                      | EXRELATIONSHIP identifierReference NEWLINE? BEGIN modelElementBody END NEWLINE
                      ;

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

EXINCLIDE           : EXCLAMATION I N C L U D E ;
WORKSPACE           : W O R K S P A C E ;
MODEL               : M O D E L ;
GROUP               : G R O U P ;
PERSON              : P E R S O N ;
PROPERTIES          : P R O P E R T I E S ;
TAGS                : T A G S ;
DESCRIPTION         : D E S C R I P T I O N ;
NAME                : N A M E ;
IDENTIFIERSKEY      : EXCLAMATION I D E N T I F I E R S ;
DOCS                : EXCLAMATION D O C S ;
ADRS                : EXCLAMATION A D R S ;
URL                 : U R L ;
TECHNOLOGY          : T E C H N O L O G Y ;
PERSPECTIVES        : P E R S P E C T I V E S ;
SOFTWARESYSTEM      : S O F T W A R E S Y S T E M ;
CONTAINER           : C O N T A I N E R ;
COMPONENT           : C O M P O N E N T ;
ELEMENT             : E L E M E N T ;
DEPLOYMENTENVIRONMENT : D E P L O Y M E N T E N V I R O N M E N T ;
DEPLOYMENTGROUP     : D E P L O Y M E N T G R O U P ;
DEPLOYMENTNODE      : D E P L O Y M E N T N O D E ;
INSTANCES           : I N S T A N C E S ;
INFRASTRUCTURENODE  : I N F R A S T R U C T U R E N O D E ;
HEALTHCHECK         : H E A L T H C H E C K ;
SOFTWARESYSTEMINSTANCE : S O F T W A R E S Y S T E M I N S T A N C E ;
CONTAINERINSTANCE   : C O N T A I N E R I N S T A N C E ;
VIEWS               : V I E W S ;
SYSTEMLANDSCAPE     : S Y S T E M L A N D S C A P E ;
INCLUDE             : I N C L U D E ;
EXCLUDE             : E X C L U D E ;
AUTOLAYOUT          : A U T O L A Y O U T ;
DEFAULT             : D E F A U L T ;
ANIMATION           : A N I M A T I O N ;
TITLE               : T I T L E ;
SYSTEMCONTEXT       : S Y S T E M C O N T E X T ;
FILTERED            : F I L T E R E D ;
DEPLOYMENT          : D E P L O Y M E N T ;
CUSTOM              : C U S T O M ;
IMAGE               : I M A G E ;
PLANTUML            : P L A N T U M L ;
MERMAID             : M E R M A I D ;
KROKI               : K R O K I ;
DYNAMIC             : D Y N A M I C ;
STYLES              : S T Y L E S ;
SHAPE               : S H A P E ;
ICON                : I C O N ;
WIDTH               : W I D T H ;
HEIGHT              : H E I G H T ;
COLOR               : C O L O R ;
STROKE              : S T R O K E ;
STROKEWIDTH         : S T R O K E W I D T H ;
FONTSIZE            : F O N T S I Z E ;
OPACITY             : O P A C I T Y ;
METADATA            : M E T A D A T A ;
BACKGROUND          : B A C K G R O U N D ;
BORDER              : B O R D E R ;
WRELATIONSHIP       : R E L A T I O N S H I P ;
THICKNESS           : T H I C K N E S S ;
STYLE               : S T Y L E ;
ROUTING             : R O U T I N G ;
POSITION            : P O S I T I O N ;
THEME               : T H E M E ;
THEMES              : T H E M E S ;
BRANDING            : B R A N D I N G ;
LOGO                : L O G O ;
FONT                : F O N T ;
TERMINOLOGY         : T E R M I N O L O G Y ;
SCOPE               : S C O P E ;
VISIBILITY          : V I S I B I L I T Y ;
USERS               : U S E R S ;
CONFIGURATION       : C O N F I G U R A T I O N ;
EXREF               : EXCLAMATION R E F;
EXELEMENT           : EXCLAMATION E L E M E N T ;
EXRELATIONSHIP      : EXCLAMATION R E L A T I O N S H I P ;
EXELEMENTS          : EXCLAMATION E L E M E N T S ;
EXRELATIONSHIPS     : EXCLAMATION R E L A T I O N S H I P S ;



WORD                : (LOWERCASE | UPPERCASE | NUMBER | '-' | '.' | '_' | ':' | '#')+ ;
PATH                : (LOWERCASE | UPPERCASE | NUMBER | '\\' | '/' | ':' | '.' | '-' | '_')+ ;
URLVALUE            : (LOWERCASE | UPPERCASE | NUMBER | '/' | ':' | '.' | '-' | '_')+ ; // TODO all allowed url characters
TEXT                : '"' .*? '"' ;
WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ ;
BEGIN               : '{' ;
END                 : '}' ;
EXCLAMATION         : '!' ;
COMMA               : ',' ;
RELATIONSHIP        : '->' ;
COMMENT             : '# ' .*? '\n' -> skip ;
MULTILINECOMMENT    : '/*' .*? '*/' -> skip;
WILDCARD            : '*' ;

