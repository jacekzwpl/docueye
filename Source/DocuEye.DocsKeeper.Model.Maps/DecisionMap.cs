using DocuEye.DocsKeeper.Application.Model;
using System;

namespace DocuEye.DocsKeeper.Model.Maps
{
    public static class DecisionMap
    {
        public static FoundedDecision MapToFoundedDecision(this Decision source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var dest = new FoundedDecision
            {
                Id = source.Id,
                Date = source.Date,
                Status = source.Status,
                Title = source.Title,
                Links = source.Links
            };
            return dest;
        }

        public static DecisionsListItem MapToDecisionsListItem(this Decision source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var dest = new DecisionsListItem
            {
                Id = source.Id,
                DslId = source.DslId,
                WorkspaceId = source.WorkspaceId,
                DocumentationId = source.DocumentationId,
                ElementId = source.ElementId
            };
            return dest;
        }
    }
}
