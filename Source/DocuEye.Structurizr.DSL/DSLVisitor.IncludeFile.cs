using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using static System.Net.Mime.MediaTypeNames;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public void VisitIncludeFileViews([NotNull] StructurizrDSLParser.IncludeFileContext context)
        {
            var parser = this.CreateIncludeFileParser(context);
            var viewsContext = parser.views();
            this.VisitViews(viewsContext);
        }

        public void VisitIncludeFileModelBody([NotNull] StructurizrDSLParser.IncludeFileContext context)
        {
            var parser = this.CreateIncludeFileParser(context);
            var modelContext = parser.modelBody();
            this.VisitModelBody(modelContext);
        }

        public void VisitIncludeFileWorkspaceBody([NotNull] StructurizrDSLParser.IncludeFileContext context)
        {
            var parser = this.CreateIncludeFileParser(context);
            var fileContext = parser.workspaceBody();
            this.VisitWorkspaceBody(fileContext);

        }

        public void VisitIncludeFileModelGroupBody([NotNull] StructurizrDSLParser.IncludeFileContext context, string groupId)
        {
            var parser = this.CreateIncludeFileParser(context);
            var modelGroupContext = parser.modelGroupBody();
            this.VisitModelGroupBody(modelGroupContext, groupId);
            
        }

        

        public void VisitIncludeFileModelElementGroupBody([NotNull] StructurizrDSLParser.IncludeFileContext context, StructurizrModelElement parentElement, string groupId)
        {
            var parser = this.CreateIncludeFileParser(context);
            var modelElementGroupContext = parser.modelElementGroupBody();
            this.VisitModelElementGroupBody(modelElementGroupContext, parentElement,  groupId);
        }

        public void VisitIncludeFileModelElementBody([NotNull] StructurizrDSLParser.IncludeFileContext context, StructurizrModelElement parentElement, string? groupId = null)
        {
            var parser = this.CreateIncludeFileParser(context);
            var modelElementContext = parser.modelElementBody();
            this.VisitModelElementBody(modelElementContext, parentElement, groupId);
        }

        protected (DSLVisitor visitor, StructurizrDSLParser parser) CreateIncludeFileParserAndvisitor([NotNull] StructurizrDSLParser.IncludeFileContext context)
        {
            var includeFileValueContext = context.includeFileValue();
            var filePath = includeFileValueContext.GetText().Trim('"');
            var parser = CreateParserFromFile(filePath);
            var visitor = new DSLVisitor(this.workspace);
            return (visitor, parser);
        }

        protected StructurizrDSLParser CreateIncludeFileParser([NotNull] StructurizrDSLParser.IncludeFileContext context)
        {
            var includeFileValueContext = context.includeFileValue();
            var filePath = includeFileValueContext.GetText().Trim('"');

            Uri? validatedUri;
            var isValid = Uri.TryCreate(filePath, UriKind.Absolute, out validatedUri);
            if(isValid)
            {
                return CreateParserFromUrl(filePath).GetAwaiter().GetResult();
            }
            else
            {
                return CreateParserFromFile(filePath);
            }

                
        }

        protected async Task<StructurizrDSLParser> CreateParserFromUrl(string filePath)
        {
            var content = await this.httpClient.GetStringAsync(filePath);
            return CreateParserFromText(content);
        }

        protected StructurizrDSLParser CreateParserFromFile(string file)
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

        public override object VisitIncludeFile([NotNull] StructurizrDSLParser.IncludeFileContext context)
        {
            throw new NotSupportedException("This method is not supported.");
        }
    }
}
