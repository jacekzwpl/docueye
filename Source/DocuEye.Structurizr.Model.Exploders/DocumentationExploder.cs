using AutoMapper;
using DocuEye.Structurizr.Json.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Model.Exploders
{
    public class DocumentationExploder
    {
        private readonly IMapper mapper;

        public DocumentationExploder(IMapper mapper)
        {
            this.mapper = mapper;
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
            return this.mapper.Map<IEnumerable<DocumentationSectionToImport>>(sections);
        }

        public IEnumerable<DecisionToImport> ExplodeDecisions(IEnumerable<StructurizrJsonDecision>? decisions, string documentationId, string? elementId = null)
        {
            if(decisions == null)
            {
                return Enumerable.Empty<DecisionToImport>();
            }
            var elements = decisions.Select(c =>
            {
                var element = this.mapper.Map<DecisionToImport>(c);
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
                var element = this.mapper.Map<ImageToImport>(c);
                element.DocumentationId = documentationId;
                return element;
            });
            return elements;
        }
    }
}
