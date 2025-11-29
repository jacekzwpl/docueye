using Antlr4.Runtime;
using DocuEye.Structurizr.DSL.Model;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor : StructurizrDSLBaseVisitor<object>, IDisposable
    {
        private StructurizrWorkspace workspace;
        private HttpClient httpClient;

        public StructurizrWorkspace Workspace { get => this.workspace; }

        public DSLVisitor() { 
            this.workspace = new StructurizrWorkspace();
            this.httpClient = new HttpClient();
        }

        public DSLVisitor(StructurizrWorkspace workspace)
        {
            this.workspace = workspace;
            this.httpClient = new HttpClient();
        }

        public DSLVisitor(StructurizrWorkspace workspace, HttpClient httpClient)
        {
            this.workspace = workspace;
            this.httpClient = httpClient;
        }

        protected StructurizrDSLExpressionsParser CreateExpressionsParserFromText(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            StructurizrDSLExpressionsLexer lexer = new StructurizrDSLExpressionsLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            return new StructurizrDSLExpressionsParser(commonTokenStream);
        }

        public void Dispose()
        {
            this.httpClient.Dispose();
        }
    }
}
