using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using System;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class ViewConfigurationToImportMap
    {
        public static DocuEye.WorkspacesKeeper.Model.ViewConfiguration MapToViewConfiguration(this ViewConfigurationToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new DocuEye.WorkspacesKeeper.Model.ViewConfiguration
            {
                Terminology = source.Terminology?.ToTerminology(),
                ElementStyles = source.ElementStyles.ToElementStyles(),
                RelationshipStyles = source.RelationshipStyles.ToRelationshipStyles(),
                Themes = source.Themes,
                GroupSeparator = source.GroupSeparator
            };
        }

        

       

        
    }
}
