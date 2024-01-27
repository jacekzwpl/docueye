using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrImage
    {
        /// <summary>
        /// The name of the image.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The (base64 encoded) content of the image.
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// The image MIME type (e.g. \"image/png\").
        /// </summary>
        public string? Type { get; set; }
       
    }
}
