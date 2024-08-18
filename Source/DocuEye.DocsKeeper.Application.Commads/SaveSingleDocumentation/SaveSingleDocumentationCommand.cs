using DocuEye.DocsKeeper.Model;
using MediatR;

namespace DocuEye.DocsKeeper.Application.Commads.SaveSingleDocumentation
{
    public class SaveSingleDocumentationCommand : IRequest
    {
        public Documentation Documentation { get; set; } = null!;

        public SaveSingleDocumentationCommand(Documentation documentation)
        {
            Documentation = documentation;
        }
    }
}
