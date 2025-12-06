using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonImageViewMap
    {
        public static ViewToImport ToViewToImport(this StructurizrJsonImageView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var view = new ViewToImport
            {
                ViewType = ViewType.ImageView,
                Key = source.Key ?? string.Empty,
                Title = source.Title,
                Description = source.Description,
                Content = source.Content,
                ContentType = source.ContentType
                
            };
            return view;
        }
    }
}
