using DocuEye.Infrastructure.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Commads.SaveDocumentationFile
{
    public class SaveDocumentationFileCommand : ICommand
    {
        public string WorkspaceId { get; set; } = null!;
        public string ElementId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string DocumentationType { get; set; } = null!;


        public SaveDocumentationFileCommand(string workspaceId, string elementId, string content, string name, string documentationType)
        {
            this.WorkspaceId = workspaceId;
            this.ElementId = elementId;
            this.Content = content;
            this.Name = name;
            this.DocumentationType = documentationType;
        }
    }
}
