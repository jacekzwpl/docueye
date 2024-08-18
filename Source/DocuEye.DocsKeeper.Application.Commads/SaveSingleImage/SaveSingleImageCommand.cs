using DocuEye.DocsKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Commads.SaveSingleImage
{
    public class SaveSingleImageCommand : IRequest
    {
        public Image Image { get; set; } = null!;

        public SaveSingleImageCommand(Image image)
        {
            this.Image = image;
        }
    }
}
