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
        public override StructurizrRelationshipStyle VisitRelationshipStyle([NotNull] StructurizrDSLParser.RelationshipStyleContext context)
        {
            var tag = context.tag().GetText().Trim('"');
            var style = new StructurizrRelationshipStyle(tag);
            var bodyContext = context.relationshipStyleBody();
            if (bodyContext != null)
            {
                var bodyStyle = (StructurizrRelationshipStyle)this.VisitRelationshipStyleBody(bodyContext);
                style.Width = bodyStyle.Width;
                style.Color = bodyStyle.Color;
                style.FontSize = bodyStyle.FontSize;
                style.Opacity = bodyStyle.Opacity;
                style.Thickness = bodyStyle.Thickness;
                style.Style = bodyStyle.Style;
                style.Routing = bodyStyle.Routing;
                style.Position = bodyStyle.Position;
                style.Properties = bodyStyle.Properties;
            }

            return style;
        }

        public override StructurizrRelationshipStyle VisitRelationshipStyleBody([NotNull] StructurizrDSLParser.RelationshipStyleBodyContext context)
        {
            var style = new StructurizrRelationshipStyle("");

            var colorContext = context.styleColorBlock();
            if (colorContext != null && colorContext.Length > 0)
            {
                style.Color = (string)this.VisitStyleColorBlock(colorContext.First());
            }

            var fontSizeContext = context.styleFontSizeBlock();
            if (fontSizeContext != null && fontSizeContext.Length > 0)
            {
                style.FontSize = (int)this.VisitStyleFontSizeBlock(fontSizeContext.First());
            }

            var widthContext = context.styleWidthBlock();
            if (widthContext != null && widthContext.Length > 0)
            {
                style.Width = (int)this.VisitStyleWidthBlock(widthContext.First());
            }

            var opacityContext = context.styleOpacityBlock();
            if (opacityContext != null && opacityContext.Length > 0)
            {
                style.Opacity = (int)this.VisitStyleOpacityBlock(opacityContext.First());
            }

            var thicknessContext = context.styleThicknessBlock();
            if (thicknessContext != null && thicknessContext.Length > 0)
            {
                style.Thickness = (int)this.VisitStyleThicknessBlock(thicknessContext.First());
            }

            var styleContext = context.styleStyleBlock();
            if (styleContext != null && styleContext.Length > 0)
            {
                style.Style = (string)this.VisitStyleStyleBlock(styleContext.First());
            }

            var routingContext = context.styleRoutingBlock();
            if (routingContext != null && routingContext.Length > 0)
            {
                style.Routing = (string)this.VisitStyleRoutingBlock(routingContext.First());
            }

            var positionContext = context.stylePositionBlock();
            if (positionContext != null && positionContext.Length > 0)
            {
                style.Position = (int)this.VisitStylePositionBlock(positionContext.First());
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
