using MediatR;

namespace DocuEye.DocsKeeper.Application.Commands.SaveOpenApiFile
{
    public class SaveOpenApiFileCommand : IRequest
    {
        public string WorkspaceId { get; set; } = null!;
        public string ElementId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Name { get; set; } = null!;


        public SaveOpenApiFileCommand(string workspaceId, string elementId, string content, string name)
        {
            this.WorkspaceId = workspaceId;
            this.ElementId = elementId;
            this.Content = content;
            this.Name = name;
        }
    }
}
