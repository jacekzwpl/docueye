using Antlr4.Runtime;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class BaseTests
    {
        protected StructurizrDSLParser CreateParser(string file)
        {
            string text = File.ReadAllText(file);
            return CreateParserFromText(text);
        }

        protected StructurizrDSLParser CreateParserFromText(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            StructurizrDSLLexer lexer = new StructurizrDSLLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            return new StructurizrDSLParser(commonTokenStream);

        }
    }
}
