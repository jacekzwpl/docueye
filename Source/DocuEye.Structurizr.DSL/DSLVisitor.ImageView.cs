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
        public override StructurizrImageView VisitImageView([NotNull] StructurizrDSLParser.ImageViewContext context)
        {
            string elementIdentifier;

            var identifierReference = context.imageViewContext();
            if (identifierReference != null)
            {
                elementIdentifier = identifierReference.GetText().Trim('"');
            }
            else
            {
                throw new Exception(
                    string.Format(
                        "Context identifier is missing at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
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

            var view = new StructurizrImageView(elementIdentifier, identifier);

            var viewBodyContext = context.imageViewBody();
            if (viewBodyContext != null)
            {
                var viewBody = this.VisitImageViewBody(viewBodyContext);
                view.Properties = viewBody.Properties;
                view.Title = viewBody.Title;
                view.ImageSource = viewBody.ImageSource;
                view.PlantUMLSource = viewBody.PlantUMLSource;
                view.MermaidSource = viewBody.MermaidSource;
                view.KrokiConfig = viewBody.KrokiConfig;
                view.Description = viewBody.Description;
                view.Default = viewBody.Default;

            }
            return view;
        }

        public override StructurizrImageView VisitImageViewBody([NotNull] StructurizrDSLParser.ImageViewBodyContext context)
        {
            var view = new StructurizrImageView("", "");
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

            var imageContext = context.imageBlock();
            view.ImageSource = this.VisitImageBlocks(imageContext);

            var plantumlContext = context.plantumlBlock();
            view.PlantUMLSource = this.VisitPlantumlBlocks(plantumlContext);

            var mermaidContext = context.mermaidBlock();
            view.MermaidSource = this.VisitMermaidBlocks(mermaidContext);

            var krokiContext = context.krokiBlock();
            view.KrokiConfig = this.VisitKrokiBlocks(krokiContext);


            var defaultContext = context.defaultBlock();
            if (defaultContext != null && defaultContext.Length > 0)
            {
                view.Default = true;
            }


            return view;
        }
    }
}
