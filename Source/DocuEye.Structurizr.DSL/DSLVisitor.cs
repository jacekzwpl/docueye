using Antlr4.Runtime;
using DocuEye.Structurizr.DSL.Model;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor : StructurizrDSLBaseVisitor<object>, IDisposable
    {
        private StructurizrWorkspace workspace;
        private HttpClient httpClient;
        private string workspaceDirectory;

        public StructurizrWorkspace Workspace { get => this.workspace; }

        public DSLVisitor(string workspaceDirectory = "") { 
            this.workspace = new StructurizrWorkspace();
            this.httpClient = new HttpClient();
            this.workspaceDirectory = workspaceDirectory;
        }

        public DSLVisitor(StructurizrWorkspace workspace, string workspaceDirectory = "")
        {
            this.workspace = workspace;
            this.httpClient = new HttpClient();
            this.workspaceDirectory = workspaceDirectory;
        }

        public DSLVisitor(StructurizrWorkspace workspace, HttpClient httpClient, string workspaceDirectory = "")
        {
            this.workspace = workspace;
            this.httpClient = httpClient;
            this.workspaceDirectory = workspaceDirectory;
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
