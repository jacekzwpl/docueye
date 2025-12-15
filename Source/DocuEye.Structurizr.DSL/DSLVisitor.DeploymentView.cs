using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override StructurizrDeploymentView VisitDeploymentView([NotNull] StructurizrDSLParser.DeploymentViewContext context)
        {
            string elementIdentifier;

            var deploymentViewContext = context.deploymentViewContext();
            if (deploymentViewContext != null)
            {
                elementIdentifier = deploymentViewContext.GetText().Trim('"');
            }
            else
            {
                throw new Exception(
                    string.Format(
                        "Software system identifier is missing at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            string environemnt;
            var environmentContext = context.environmentReference();
            if (environmentContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Environment is missing at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            else
            {
                if (environmentContext.TEXT() != null)
                {
                    var environemntName = environmentContext.GetText().Trim('"');
                    var env = this.workspace.Model.DeploymentEnvironments.FirstOrDefault(o => o.Name == environemntName);
                    if(env == null)
                    {
                        throw new Exception(
                            string.Format(
                                "Environment with name {0} does not exist in the model at {1}:{2}",
                                environemntName,
                                context.Start.Line,
                                context.Start.Column));
                    }
                    environemnt = env.Identifier;
                }
                else
                {
                    environemnt = environmentContext.GetText().Trim('"');
                }
            
                
            }


            string identifier;
            var identifierContext = context.key();
            if (identifierContext == null)
            {
                identifier = Guid.NewGuid().ToString();
            }
            else
            {
                identifier = identifierContext.GetText().Trim('"');
            }

            string? descritpiton;
            var descriptionContext = context.description();
            if (descriptionContext == null)
            {
                descritpiton = null;
            }
            else
            {
                descritpiton = descriptionContext.GetText().Trim('"');
            }

            var view = new StructurizrDeploymentView(elementIdentifier, environemnt, identifier, descritpiton);

            var viewBodyContext = context.deploymentViewBody();
            if (viewBodyContext != null)
            {
                var viewBody = this.VisitDeploymentViewBody(viewBodyContext);
                view.Properties = viewBody.Properties;
                view.Title = viewBody.Title;
                if (view.Description == null)
                {
                    view.Description = viewBody.Description;
                }
                view.Include = viewBody.Include;
                view.Exclude = viewBody.Exclude;
                view.Animation = viewBody.Animation;
                view.AutomaticLayout = viewBody.AutomaticLayout;
                view.Default = viewBody.Default;

            }
            return view;
        }

        public override StructurizrDeploymentView VisitDeploymentViewBody([NotNull] StructurizrDSLParser.DeploymentViewBodyContext context)
        {
            var view = new StructurizrDeploymentView("", "", "");
            var propertiesBlock = context.properties();
            if (propertiesBlock != null)
            {
                view.Properties = this.VisitPropertiesBlocks(propertiesBlock);
            }
            var titleBlock = context.titleBlock();
            if (titleBlock != null)
            {
                view.Title = this.VisitTitleBlocks(titleBlock);
            }

            var descriptionBlocks = context.descriptionBlock();
            view.Description = this.VisitDescriptionBlocks(descriptionBlocks);

            var includeContext = context.includeBlock();
            view.Include = this.VisitIncludeBlocks(includeContext);

            var excludeContext = context.excludeBlock();
            view.Exclude = this.VisitExcludeBlocks(excludeContext);


            var animationContext = context.animation();
            if (animationContext != null && animationContext.Length > 0)
            {
                view.Animation = (IEnumerable<IEnumerable<string>>)this.VisitAnimation(animationContext.First());
            }

            var autoLayoutContext = context.autoLayoutBlock();
            if (autoLayoutContext != null && autoLayoutContext.Length > 0)
            {
                view.AutomaticLayout = (StructurizrAutoLayout)this.VisitAutoLayoutBlock(autoLayoutContext.First());
            }

            var defaultContext = context.defaultBlock();
            if (defaultContext != null && defaultContext.Length > 0)
            {
                view.Default = true;
            }


            return view;
        }
    }
}
