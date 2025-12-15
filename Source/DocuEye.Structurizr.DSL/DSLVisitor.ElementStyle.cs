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
        public override StructurizrElementStyle VisitElementStyle([NotNull] StructurizrDSLParser.ElementStyleContext context)
        {
            var tag = context.tag().GetText().Trim('"');
            var style = new StructurizrElementStyle(tag);
            var bodyContext = context.elementStyleBody();
            if (bodyContext != null)
            {
                var bodyStyle = (StructurizrElementStyle)this.VisitElementStyleBody(bodyContext);
                style.Shape = bodyStyle.Shape;
                style.Icon = bodyStyle.Icon;
                style.Width = bodyStyle.Width;
                style.Height = bodyStyle.Height;
                style.Background = bodyStyle.Background;
                style.Color = bodyStyle.Color;
                style.Stroke = bodyStyle.Stroke;
                style.StrokeWidth = bodyStyle.StrokeWidth;
                style.FontSize = bodyStyle.FontSize;
                style.Border = bodyStyle.Border;
                style.Opacity = bodyStyle.Opacity;
                style.Metadata = bodyStyle.Metadata;
                style.Description = bodyStyle.Description;
                style.Properties = bodyStyle.Properties;
            }

            return style;
        }

        public override StructurizrElementStyle VisitElementStyleBody([NotNull] StructurizrDSLParser.ElementStyleBodyContext context)
        {

            var style = new StructurizrElementStyle("");
            var shapeContext = context.styleShapeBlock();
            if (shapeContext != null && shapeContext.Length > 0)
            {
                style.Shape = (string)this.VisitStyleShapeBlock(shapeContext.First());
            }

            var iconContext = context.styleIconBlock();
            if (iconContext != null && iconContext.Length > 0)
            {
                style.Icon = (string)this.VisitStyleIconBlock(iconContext.First());
            }

            var widthContext = context.styleWidthBlock();
            if (widthContext != null && widthContext.Length > 0)
            {
                style.Width = (int)this.VisitStyleWidthBlock(widthContext.First());
            }

            var heightContext = context.styleHeightBlock();
            if (heightContext != null && heightContext.Length > 0)
            {
                style.Height = (int)this.VisitStyleHeightBlock(heightContext.First());
            }

            var backgroundContext = context.styleBackgroundBlock();
            if (backgroundContext != null && backgroundContext.Length > 0)
            {
                style.Background = (string)this.VisitStyleBackgroundBlock(backgroundContext.First());
            }

            var colorContext = context.styleColorBlock();
            if (colorContext != null && colorContext.Length > 0)
            {
                style.Color = (string)this.VisitStyleColorBlock(colorContext.First());
            }

            var strokeContext = context.styleStrokeBlock();
            if (strokeContext != null && strokeContext.Length > 0)
            {
                style.Stroke = (string)this.VisitStyleStrokeBlock(strokeContext.First());
            }

            var strokeWidthContext = context.styleStrokeWidthBlock();
            if (strokeWidthContext != null && strokeWidthContext.Length > 0)
            {
                style.StrokeWidth = (int)this.VisitStyleStrokeWidthBlock(strokeWidthContext.First());
            }

            var fontSizeContext = context.styleFontSizeBlock();
            if (fontSizeContext != null && fontSizeContext.Length > 0)
            {
                style.FontSize = (int)this.VisitStyleFontSizeBlock(fontSizeContext.First());
            }

            var borderContext = context.styleBorderBlock();
            if (borderContext != null && borderContext.Length > 0)
            {
                style.Border = (string)this.VisitStyleBorderBlock(borderContext.First());
            }

            var opacityContext = context.styleOpacityBlock();
            if (opacityContext != null && opacityContext.Length > 0)
            {
                style.Opacity = (int)this.VisitStyleOpacityBlock(opacityContext.First());
            }

            var metadataContext = context.styleMetadataBlock();
            if (metadataContext != null && metadataContext.Length > 0)
            {
                style.Metadata = (bool)this.VisitStyleMetadataBlock(metadataContext.First());
            }

            var descriptionContext = context.styleDescriptionBlock();
            if (descriptionContext != null && descriptionContext.Length > 0)
            {
                style.Description = (bool)this.VisitStyleDescriptionBlock(descriptionContext.First());
            }

            var propertiesContext = context.properties();
            if (propertiesContext != null)
            {
                style.Properties = this.VisitPropertiesBlocks(propertiesContext);
            }

            return style;
        }
    }
}
