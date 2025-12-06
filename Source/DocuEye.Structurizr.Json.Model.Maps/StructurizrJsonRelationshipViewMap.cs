using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonRelationshipViewMap
    {
        public static RelationshipInViewToImport ToRelationshipInViewToImport(this StructurizrJsonRelationshipView source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new RelationshipInViewToImport
            {
                StructurizrId = source.Id ?? string.Empty,
                Description = source.Description,
                Response = source.Response,
                Order = source.Order
            };
        }
    }
}
