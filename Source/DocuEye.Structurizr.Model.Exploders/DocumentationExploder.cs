using DocuEye.Structurizr.Json.Model;
using DocuEye.Structurizr.Json.Model.Maps;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Model.Exploders
{
    public class DocumentationExploder
    {


        public DocumentationExploder()
        {
        }

        public (DocumentationToImport,IEnumerable<DecisionToImport>,IEnumerable<ImageToImport>) ExplodeDocumentation(StructurizrJsonDocumentation documentation, string? elementId = null)
        {

            var documentationToImport = new DocumentationToImport()
            {
                Id = Guid.NewGuid().ToString(),
                StructurizrElementId = elementId,
                Sections = this.ExplodeDocumentationSections(documentation.Sections)
            };

            return (
                documentationToImport, 
                this.ExplodeDecisions(documentation.Decisions, documentationToImport.Id, elementId), 
                this.ExplodeImages(documentation.Images, documentationToImport.Id)
            );
        }

        public IEnumerable<DocumentationSectionToImport> ExplodeDocumentationSections(IEnumerable<StructurizrJsonDocumentationSection>? sections)
        {
            if(sections == null)
            {
                return Enumerable.Empty<DocumentationSectionToImport>();
            }
            return sections.ConvertToApiModel();
        }

        public IEnumerable<DecisionToImport> ExplodeDecisions(IEnumerable<StructurizrJsonDecision>? decisions, string documentationId, string? elementId = null)
        {
            if(decisions == null)
            {
                return Enumerable.Empty<DecisionToImport>();
            }
            var elements = decisions.Select(c =>
            {
                var element = c.ConvertToApiModel();
                element.DocumentationId = documentationId;
                element.StrucuturizrElementId = elementId;
                return element;
            });
            return elements;
        }

        public IEnumerable<ImageToImport> ExplodeImages(IEnumerable<StructurizrJsonImage>? images, string documentationId)
        {
            if(images == null)
            {
                return Enumerable.Empty<ImageToImport>();
            }
            var elements = images.Select(c =>
            {
                var element = c.ConvertToApiModel();
                element.DocumentationId = documentationId;
                return element;
            });
            return elements;
        }
    }
}
