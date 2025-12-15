using Antlr4.Runtime;
using DocuEye.Structurizr.DSL.Model;
using DocuEye.Structurizr.Json.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace DocuEye.Structurizr.DslToJson
{
    public partial class WorkspaceConverter
    {
        private StructurizrWorkspace dslWorkspace;

        private string modelGroupSeparator = "|";
        private DocumentationReader documentationReader;
        private WorkspaceSerializer workspaceSerializer;
        private string workspaceDirectory;

        public WorkspaceConverter(StructurizrWorkspace dslWorkspace, string workspaceDirectory) { 
            
            this.dslWorkspace = dslWorkspace;
            this.workspaceDirectory = workspaceDirectory;
            this.documentationReader = new DocumentationReader(workspaceDirectory);
            this.workspaceSerializer = new WorkspaceSerializer();
        }

        public void Convert(string resultFilePath)
        {
            var jsonWorkspace = this.Convert();
            var jsonText = this.workspaceSerializer.Serialize(jsonWorkspace);
            File.WriteAllText(resultFilePath, jsonText);
        }

        public StructurizrJsonWorkspace Convert()
        {
            var jsonWroskapace = new StructurizrJsonWorkspace()
            {
                Name = this.dslWorkspace.Name,
                Description = this.dslWorkspace.Description,
                Properties = this.dslWorkspace.Properties,
                Model = this.ConvertModel(this.dslWorkspace.Model),
                Views = this.ConvertViews(this.dslWorkspace.Views),
                Documentation = this.documentationReader.Read(this.dslWorkspace.Docs, this.dslWorkspace.Adrs),
                Configuration = this.ConvertWorkspaceConfiguration(this.dslWorkspace.Configuration)
            };
            return jsonWroskapace;
        }

        protected StructurizrDSLExpressionsParser CreateExpressionsParserFromText(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            StructurizrDSLExpressionsLexer lexer = new StructurizrDSLExpressionsLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            return new StructurizrDSLExpressionsParser(commonTokenStream);

        }

        private StructurizrJsonWorkspaceConfiguration ConvertWorkspaceConfiguration(StructurizrConfiguration? configuration) {
            return new StructurizrJsonWorkspaceConfiguration()
            {
                Visibility = configuration?.Visibility ?? "public",
                Users = configuration?.Users?.Select(u => new StructurizrJsonWorkspaceConfigurationUser()
                {
                    Username = u.Username,
                    Role = u.Role
                })
            };
        }

    }
}
