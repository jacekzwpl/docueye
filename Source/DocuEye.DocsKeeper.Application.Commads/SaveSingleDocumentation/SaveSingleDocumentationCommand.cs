using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Mediator.Commands;


namespace DocuEye.DocsKeeper.Application.Commads.SaveSingleDocumentation
{
    public class SaveSingleDocumentationCommand : ICommand
    {
        public Documentation Documentation { get; set; } = null!;

        public SaveSingleDocumentationCommand(Documentation documentation)
        {
            Documentation = documentation;
        }
    }
}
