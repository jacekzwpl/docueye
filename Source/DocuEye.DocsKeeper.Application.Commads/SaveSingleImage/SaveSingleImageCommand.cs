using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Mediator.Commands;



namespace DocuEye.DocsKeeper.Application.Commads.SaveSingleImage
{
    public class SaveSingleImageCommand : ICommand
    {
        public Image Image { get; set; } = null!;

        public SaveSingleImageCommand(Image image)
        {
            this.Image = image;
        }
    }
}
