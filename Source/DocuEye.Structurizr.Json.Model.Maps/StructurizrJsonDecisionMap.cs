using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonDecisionMap
    {
        public static DecisionToImport ConvertToApiModel(this StructurizrJsonDecision source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new DecisionToImport
            {
                StrucuturizrId = source.Id,
                StrucuturizrElementId = source.ElementId,
                Date = source.Date,
                Status = source.Status,
                Title = source.Title,
                Content = source.Content,
                Format = source.Format,
                Links = source.Links?.ConvertAllToApiModel(),
            };
        }
    }
}
