using MediatR;

namespace DocuEye.DocsKeeper.Application.Commands.SaveOpenApiFile
{
    public class SaveOpenApiFileCommand : IRequest
    {
        public string WorkspaceId { get; set; }
        public string ElementId { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }

        public SaveOpenApiFileCommand(string workspaceId, string elementId, string content, string name)
        {
            this.WorkspaceId = workspaceId;
            this.ElementId = elementId;
            this.Content = content;
            this.Name = name;
        }
    }
}
