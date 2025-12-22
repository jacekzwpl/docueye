using DocuEye.ViewsKeeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ViewsKeeper.Api.Model
{
    public class DynamicViewDiagram : DynamicView
    {
        public string? LayoutData { get; set; }
    }
}
