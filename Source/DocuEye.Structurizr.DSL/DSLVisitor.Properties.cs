using Antlr4.Runtime.Misc;
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
        public override object VisitProperties([NotNull] PropertiesContext context)
        {
            var objectProperties = new Dictionary<string, string>();
            var properties = context.property();
            foreach (var property in properties)
            {
                var key = property.name().GetText().Trim('"');
                var value = property.propertyValue().GetText().Trim('"');
                if (objectProperties.ContainsKey(key))
                {
                    throw new Exception(
                        string.Format(
                            "Duplicate property key '{0}' at {1}:{2}",
                            key,
                            property.Start.Line,
                            property.Start.Column));
                }
                objectProperties.Add(key, value);
            }
            return objectProperties;
        }

        public Dictionary<string, string> VisitPropertiesBlocks(PropertiesContext[]? propertiesBlocks)
        {
            var properties = new Dictionary<string, string>();
            if (propertiesBlocks == null)
            {
                return properties;
            }
            if (propertiesBlocks.Length > 1)
            {
                var duplicate = propertiesBlocks[1];
                throw new Exception(
                    string.Format(
                        "Duplicate properties block for workspace at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (propertiesBlocks.Length > 0)
            {
                return (Dictionary<string, string>)this.VisitProperties(propertiesBlocks[0]);
            }
            return properties;
        }
    }
}
