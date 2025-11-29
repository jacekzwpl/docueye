using Antlr4.Runtime;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class BaseTests
    {
        protected StructurizrDSLExpressionsParser CreateParser(string file)
        {
            string text = File.ReadAllText(file);
            return CreateParserFromText(text);
        }

        protected StructurizrDSLExpressionsParser CreateParserFromText(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            StructurizrDSLExpressionsLexer lexer = new StructurizrDSLExpressionsLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            return new StructurizrDSLExpressionsParser(commonTokenStream);

        }
    }
}