using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors
{
    public class ViewsChangeDetector
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ViewsChangeDetector(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public void DetectViewsIds(string workspaceId, IEnumerable<ViewToImport> viewsToImport, IEnumerable<BaseView> existingViews, IEnumerable<Element> existingElements, IEnumerable<Relationship> existingRelationship)
        {
            //Detect Views Ids
            var viewsIdsMap = new Dictionary<string, string>();
            var elementsDiagrams = new Dictionary<string, string>();
            foreach (var viewToImport in viewsToImport)
            {
                var existingView = existingViews.SingleOrDefault(o => o.Key == viewToImport.Key);
                var viewId = existingView == null ? Guid.NewGuid().ToString() : existingView.Id;
                var contextElement = existingElements
                    .FirstOrDefault(o => o.StructurizrId == viewToImport.StructurizrElementId);
                viewsIdsMap.Add(viewToImport.Key, viewId);
                if (contextElement != null)
                {
                    elementsDiagrams.Add(contextElement.Id, viewId);
                }
            }
        }

        public IEnumerable<SystemLandscapeView> DetectSystemLandscapeViews(string workspaceId, IEnumerable<ViewToImport> viewsToImport, Dictionary<string, string> viewsIdsMap, IEnumerable<Element> existingElements, IEnumerable<Relationship> existingRelationship, Dictionary<string, string> elementsDiagrams)
        {
            var result = new List<SystemLandscapeView>();
            foreach (var viewToImport in viewsToImport.Where(o => o.ViewType == ViewType.SystemLandscapeView))
            {
                var view = this.mapper.Map<SystemLandscapeView>(viewToImport);
                view.Id = viewsIdsMap[viewToImport.Key];
                view.WorkspaceId = workspaceId;
                view.Elements = this.DetectElementsInView(viewToImport.Elements, existingElements, elementsDiagrams);
                view.Relationships = this.DetectRelationshipsInView(viewToImport.Relationships, existingRelationship);
                result.Add(view);

            }
            return result;
        }

        public IEnumerable<ElementView> DetectElementsInView(IEnumerable<ElementInViewToImport> elementsInView, IEnumerable<Element> existingElements, Dictionary<string, string> elementsDiagrams)
        {
            var result = new List<ElementView>();
            foreach(var elementToImport in elementsInView)
            {
                var existingElement = existingElements.FirstOrDefault(o => o.StructurizrId == elementToImport.StructurizrId);
                if (existingElement == null)
                {
                    continue;
                }
                var elementView = this.mapper.Map<ElementView>(existingElement);
                elementView.X = elementToImport.X;
                elementView.Y = elementToImport.Y;
                if(elementsDiagrams.ContainsKey(existingElement.Id))
                {
                    elementView.DiagramId = elementsDiagrams[existingElement.Id];
                }


                result.Add(elementView);
            }
            return result;
        }

        public IEnumerable<RelationshipView> DetectRelationshipsInView(IEnumerable<RelationshipInViewToImport> relationshipsInView, IEnumerable<Relationship> existingRelationships)
        {
            var result = new List<RelationshipView>();
            foreach (var relationshipToImport in relationshipsInView)
            {
                var existingRelationship = existingRelationships
                    .FirstOrDefault(o => o.StructurizrId == relationshipToImport.StructurizrId);
                if(existingRelationship == null)
                {
                    continue;
                }
                var relationshipView = this.mapper.Map<RelationshipView>(existingRelationship);
                result.Add(relationshipView);
            }
            return result;
        }


    }
}
