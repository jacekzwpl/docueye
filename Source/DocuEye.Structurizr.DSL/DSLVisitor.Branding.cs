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
        public override StructurizrBranding VisitBranding([NotNull] StructurizrDSLParser.BrandingContext context)
        {
            var branding = new StructurizrBranding();
            var logoContext = context.brandingLogoBlock();
            if(logoContext != null && logoContext.Length > 0)
            {
                branding.Logo = (string)this.VisitBrandingLogoBlock(logoContext.First());
            }

            var fontContext = context.brandingFontBlock();
            if (fontContext != null && fontContext.Length > 0)
            {
                branding.Font = (string)this.VisitBrandingFontBlock(fontContext.First());
            }

            return branding;
        }

        public override object VisitBrandingLogoBlock([NotNull] StructurizrDSLParser.BrandingLogoBlockContext context)
        {
            var value = context.imageValue().GetText().Trim('"');
            return value;
        }

        public override object VisitBrandingFontBlock([NotNull] StructurizrDSLParser.BrandingFontBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            return value;
        }
    }
}
