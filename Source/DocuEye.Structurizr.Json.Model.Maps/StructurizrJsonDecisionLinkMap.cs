using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonDecisionLinkMap
    {
        public static DecisionLinkToImport ConvertToApiModel(this StructurizrJsonDecisionLink source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new DecisionLinkToImport
            {
                StructurizrId = source.Id,
                Description = source.Description,
                
            };
        }

        public static IEnumerable<DecisionLinkToImport> ConvertAllToApiModel(this IEnumerable<StructurizrJsonDecisionLink> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var result = new List<DecisionLinkToImport>();
            foreach (var item in source)
            {
                result.Add(item.ConvertToApiModel());
            }
            return result.AsEnumerable();
        }
    }
}
