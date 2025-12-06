using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonImageMap
    {
        public static ImageToImport ConvertToApiModel(this StructurizrJsonImage source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new ImageToImport
            {
                Name = source.Name,
                Content = source.Content,
                Type = source.Type
            };
        }
    }
}
