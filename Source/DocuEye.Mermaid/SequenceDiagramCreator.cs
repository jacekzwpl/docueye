using DocuEye.ViewsKeeper.Model;
using System;
using System.Text;

namespace DocuEye.Mermaid
{
    public class SequenceDiagramCreator
    {
        public string Create(DynamicView dynamicView) { 
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("sequenceDiagram");
            foreach (var element in dynamicView.Elements)
            {
                //sb.AppendLine("\t");
                sb.AppendLine($"\tparticipant {element.Id} as {element.Name}");
            }
            foreach (var relationship in dynamicView.Relationships)
            {
                string relationshipType = relationship.Response.HasValue && relationship.Response.Value ? "-->>" : "->>";
                //sb.AppendLine("\t");
                sb.AppendLine($"\t{relationship.SourceId} {relationshipType} {relationship.DestinationId}: {relationship.Description}");
            }
            return sb.ToString();
        }
    }
}
