using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override object VisitWorkspace(WorkspaceContext context)
        {
            NameContext name = context.name();
            DescriptionContext description = context.description();


            this.workspace.Name = name?.GetText().Trim('"');
            this.workspace.Description = description?.GetText().Trim('"');

            WorkspaceBodyContext workspaceBody = context.workspaceBody();
            if (workspaceBody != null)
            {
                this.VisitWorkspaceBody(workspaceBody);
            }


            return this.workspace;
        }

        public override object VisitWorkspaceBody(WorkspaceBodyContext context)
        {
            var nameBlock = context.nameBlock();
            if (nameBlock.Length > 1)
            {
                var duplicate = nameBlock[nameBlock.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate name property for workspace at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }

            if (nameBlock.Length > 0)
            {
                this.workspace.Name = nameBlock[0].name()?.GetText().Trim('"');
            }

            var descriptionBlock = context.descriptionBlock();
            if (descriptionBlock.Length > 1)
            {
                var duplicate = descriptionBlock[descriptionBlock.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate description property for workspace at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (descriptionBlock.Length > 0)
            {
                this.workspace.Description = descriptionBlock[0].description()?.GetText().Trim('"');
            }

            var propertiesBlock = context.properties();
            if (propertiesBlock.Length > 1)
            {
                var duplicate = propertiesBlock[propertiesBlock.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate properties block for workspace at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (propertiesBlock.Length > 0)
            {
                this.workspace.Properties = (Dictionary<string, string>)this.VisitProperties(propertiesBlock[0]);
            }

            var identifiersKeyword = context.identifiersKeyword();
            if (identifiersKeyword.Length > 1)
            {
                var duplicate = identifiersKeyword[identifiersKeyword.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate identifiers keyword for workspace at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (identifiersKeyword.Length > 0)
            {
                var identifiersValue = identifiersKeyword[0].identifiersValue().GetText();
                if (identifiersValue != "flat" && identifiersValue != "hierarchical")
                {
                    throw new Exception(
                        string.Format(
                            "Invalid identifiers value '{0}' at {1}:{2}",
                            identifiersValue,
                            identifiersKeyword[0].Start.Line,
                            identifiersKeyword[0].Start.Column));
                }
                this.workspace.Identifiers = identifiersValue;

            }

            var docsKeywords = context.docsKeyword();
            this.workspace.Docs = this.VisitDocsKeyword(docsKeywords);

            var adrsKeywords = context.adrsKeyword();
            this.workspace.Adrs = this.VisitAdrsKeyword(adrsKeywords);

            var modelBlock = context.model();
            if (modelBlock.Length > 1)
            {
                var duplicate = modelBlock[modelBlock.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate model block for workspace at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (modelBlock.Length > 0)
            {
                this.VisitModel(modelBlock[0]);
            }

            var includeFileContexts = context.includeFile();
            foreach (var includeFileContext in includeFileContexts)
            {
                this.VisitIncludeFileWorkspaceBody(includeFileContext);
            }

            var viewsBlock = context.views();
            if (viewsBlock.Length > 1)
            {
                var duplicate = viewsBlock[viewsBlock.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate views block for workspace at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (viewsBlock.Length > 0)
            {
                this.workspace.Views = this.VisitViews(viewsBlock[0]);
            }

            var configurationBlock = context.configuration();
            if (configurationBlock.Length > 1)
            {
                var duplicate = configurationBlock[configurationBlock.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate configuration block for workspace at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (configurationBlock.Length > 0)
            {
                this.workspace.Configuration = this.VisitConfiguration(configurationBlock[0]);
            }

            return this.workspace;
        }
    }
}
