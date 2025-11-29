using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override object VisitStyleShapeBlock([NotNull] StructurizrDSLParser.StyleShapeBlockContext context)
        {
            var allowedValues = new string[] {
                "box", "roundedbox", "circle",
                "ellipse","hexagon","cylinder","pipe",
                "person","robot","folder","webbrowser",
                "mobiledeviceportrait","mobiledevicelandscape",
                "component"
            };

            var value = context.value().GetText().Trim('"');
            
            if (!allowedValues.Contains(value.ToLower()))
            {
                throw new Exception(
                    string.Format(
                        "Invalid shape value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return value;
        }
    }
}
