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

        public (Dictionary<string, string>, Dictionary<string, string>) DetectViewsIds(IEnumerable<ViewToImport> viewsToImport, IEnumerable<BaseView> existingViews, IEnumerable<Element> existingElements)
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
                if (contextElement != null && !elementsDiagrams.ContainsKey(contextElement.Id))
                {
                    elementsDiagrams.Add(contextElement.Id, viewId);
                }
            }
            return (viewsIdsMap, elementsDiagrams);
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
            
        public IEnumerable<SystemContextView> DetectSystemContextViews(string workspaceId, IEnumerable<ViewToImport> viewsToImport, Dictionary<string, string> viewsIdsMap, IEnumerable<Element> existingElements, IEnumerable<Relationship> existingRelationship, Dictionary<string, string> elementsDiagrams)
        {
            var result = new List<SystemContextView>();
            foreach (var viewToImport in viewsToImport.Where(o => o.ViewType == ViewType.SystemContextView))
            {
                var view = this.mapper.Map<SystemContextView>(viewToImport);
                view.Id = viewsIdsMap[viewToImport.Key];
                view.WorkspaceId = workspaceId;
                if(!string.IsNullOrEmpty(viewToImport.StructurizrElementId))
                {
                    var contextElement = existingElements.FirstOrDefault(o => o.StructurizrId == viewToImport.StructurizrElementId);
                    if (contextElement != null)
                    {
                        view.SoftwareSystemId = contextElement.Id;
                    }
                }
                view.Elements = this.DetectElementsInView(viewToImport.Elements, existingElements, elementsDiagrams);
                view.Relationships = this.DetectRelationshipsInView(viewToImport.Relationships, existingRelationship);
                result.Add(view);

            }
            return result;
        }
      

        public IEnumerable<ContainerView> DetectContainerViews(string workspaceId, IEnumerable<ViewToImport> viewsToImport, Dictionary<string, string> viewsIdsMap, IEnumerable<Element> existingElements, IEnumerable<Relationship> existingRelationship, Dictionary<string, string> elementsDiagrams)
        {
            var result = new List<ContainerView>();
            foreach (var viewToImport in viewsToImport.Where(o => o.ViewType == ViewType.ContainerView))
            {
                var view = this.mapper.Map<ContainerView>(viewToImport);
                view.Id = viewsIdsMap[viewToImport.Key];
                view.WorkspaceId = workspaceId;
                if (!string.IsNullOrEmpty(viewToImport.StructurizrElementId))
                {
                    var contextElement = existingElements.FirstOrDefault(o => o.StructurizrId == viewToImport.StructurizrElementId);
                    if (contextElement != null)
                    {
                        view.SoftwareSystemId = contextElement.Id;
                    }
                }
                view.Elements = this.DetectElementsInView(viewToImport.Elements, existingElements, elementsDiagrams);
                view.Relationships = this.DetectRelationshipsInView(viewToImport.Relationships, existingRelationship);
                result.Add(view);

            }
            return result;
        }

        public IEnumerable<ComponentView> DetectComponentViews(string workspaceId, IEnumerable<ViewToImport> viewsToImport, Dictionary<string, string> viewsIdsMap, IEnumerable<Element> existingElements, IEnumerable<Relationship> existingRelationship, Dictionary<string, string> elementsDiagrams)
        {
            var result = new List<ComponentView>();
            foreach (var viewToImport in viewsToImport.Where(o => o.ViewType == ViewType.ComponentView))
            {
                var view = this.mapper.Map<ComponentView>(viewToImport);
                view.Id = viewsIdsMap[viewToImport.Key];
                view.WorkspaceId = workspaceId;
                if (!string.IsNullOrEmpty(viewToImport.StructurizrElementId))
                {
                    var contextElement = existingElements.FirstOrDefault(o => o.StructurizrId == viewToImport.StructurizrElementId);
                    if (contextElement != null)
                    {
                        view.ContainerId = contextElement.Id;
                    }
                }
                view.Elements = this.DetectElementsInView(viewToImport.Elements, existingElements, elementsDiagrams);
                view.Relationships = this.DetectRelationshipsInView(viewToImport.Relationships, existingRelationship);
                result.Add(view);

            }
            return result;
        }

        public IEnumerable<DeploymentView> DetectDeploymentViews(string workspaceId, IEnumerable<ViewToImport> viewsToImport, Dictionary<string, string> viewsIdsMap, IEnumerable<Element> existingElements, IEnumerable<Relationship> existingRelationship, Dictionary<string, string> elementsDiagrams)
        {
            var result = new List<DeploymentView>();
            foreach (var viewToImport in viewsToImport.Where(o => o.ViewType == ViewType.DeploymentView))
            {
                var view = this.mapper.Map<DeploymentView>(viewToImport);
                view.Id = viewsIdsMap[viewToImport.Key];
                view.WorkspaceId = workspaceId;
                if (!string.IsNullOrEmpty(viewToImport.StructurizrElementId))
                {
                    var contextElement = existingElements.FirstOrDefault(o => o.StructurizrId == viewToImport.StructurizrElementId);
                    if (contextElement != null)
                    {
                        view.SoftwareSystemId = contextElement.Id;
                    }
                }
                view.Elements = this.DetectElementsInView(viewToImport.Elements, existingElements, elementsDiagrams);
                view.Relationships = this.DetectRelationshipsInView(viewToImport.Relationships, existingRelationship);
                result.Add(view);

            }
            return result;
        }

        public IEnumerable<ImageView> DetectImageViews(string workspaceId, IEnumerable<ViewToImport> viewsToImport, Dictionary<string, string> viewsIdsMap, IEnumerable<Element> existingElements)
        {
            var result = new List<ImageView>();
            foreach (var viewToImport in viewsToImport.Where(o => o.ViewType == ViewType.ImageView))
            {
                var view = this.mapper.Map<ImageView>(viewToImport);
                view.Id = viewsIdsMap[viewToImport.Key];
                view.WorkspaceId = workspaceId;
                if (!string.IsNullOrEmpty(viewToImport.StructurizrElementId))
                {
                    var contextElement = existingElements.FirstOrDefault(o => o.StructurizrId == viewToImport.StructurizrElementId);
                    if (contextElement != null)
                    {
                        view.ElementId = contextElement.Id;
                    }
                }
                result.Add(view);

            }
            return result;
        }

        public IEnumerable<DynamicView> DetectDynamicViews(string workspaceId, IEnumerable<ViewToImport> viewsToImport, Dictionary<string, string> viewsIdsMap, IEnumerable<Element> existingElements, IEnumerable<Relationship> existingRelationships, Dictionary<string, string> elementsDiagrams) {
            var result = new List<DynamicView>();
            foreach (var viewToImport in viewsToImport.Where(o => o.ViewType == ViewType.DynamicView))
            {
                var view = this.mapper.Map<DynamicView>(viewToImport);
                view.Id = viewsIdsMap[viewToImport.Key];
                view.WorkspaceId = workspaceId;
                if (!string.IsNullOrEmpty(viewToImport.StructurizrElementId))
                {
                    var contextElement = existingElements.FirstOrDefault(o => o.StructurizrId == viewToImport.StructurizrElementId);
                    if (contextElement != null)
                    {
                        view.ElementId = contextElement.Id;
                    }
                }
                view.Elements = this.DetectElementsInView(viewToImport.Elements, existingElements, elementsDiagrams);
                view.Relationships = this.DetectDynamicRelationshipsView(viewToImport.Relationships, existingRelationships);
                result.Add(view);
            }
            return result;

        }

        public IEnumerable<FilteredView> DetectFilteredViews(
            string workspaceId, 
            IEnumerable<ViewToImport> viewsToImport, 
            Dictionary<string, string> viewsIdsMap,
            IEnumerable<SystemLandscapeView> systemLandscapeViews,
            IEnumerable<SystemContextView> systemContextViews,
            IEnumerable<ContainerView> containerViews,
            IEnumerable<ComponentView> componentViews)
        {
            var result = new List<FilteredView>();
            foreach (var viewToImport in viewsToImport.Where(o => o.ViewType == ViewType.FilteredView))
            {
                var view = this.mapper.Map<FilteredView>(viewToImport);
                view.Id = viewsIdsMap[viewToImport.Key];
                view.WorkspaceId = workspaceId;
                var baseView = this.GetBaseForFilteredView(view.BaseViewKey, systemLandscapeViews, systemContextViews, containerViews, componentViews);
                if(baseView == null)
                {
                   continue;
                }
                if (view.Mode?.ToLower() == "exclude")
                {
                    if (!view.Tags.Contains("*"))
                    {
                        view.Elements = baseView.Elements
                            .Where(o => !o.Tags.Any(tag => view.Tags.Contains(tag))).ToArray();
                        view.Relationships = baseView.Relationships
                            .Where(o => !o.Tags.Any(tag => view.Tags.Contains(tag))).ToArray();
                    }else
                    {
                        view.Elements = Enumerable.Empty<ElementView>();
                        view.Relationships = Enumerable.Empty<RelationshipView>();
                    }
                }
                else
                {
                    if (!view.Tags.Contains("*"))
                    {
                        view.Elements = baseView.Elements
                            .Where(o => o.Tags.Any(tag => view.Tags.Contains(tag))).ToArray();
                        view.Relationships = baseView.Relationships
                            .Where(o => o.Tags.Any(tag => view.Tags.Contains(tag))).ToArray();
                    }
                    else
                    {
                        view.Elements = baseView.Elements.ToArray();
                        view.Relationships = baseView.Relationships.ToArray();
                    }
                }
                result.Add(view);
            }
            return result;
        }

        private StaticDiagramView? GetBaseForFilteredView(
            string baseViewKey, 
            IEnumerable<SystemLandscapeView> systemLandscapeViews,
            IEnumerable<SystemContextView> systemContextViews,
            IEnumerable<ContainerView> containerViews,
            IEnumerable<ComponentView> componentViews)
        {
            StaticDiagramView? view = systemLandscapeViews.FirstOrDefault(o => o.Key == baseViewKey);
            if (view != null)
            {
                return view;
            }

            view = systemContextViews.FirstOrDefault(o => o.Key == baseViewKey);
            if (view != null)
            {
                return view;
            }

            view = containerViews.FirstOrDefault(o => o.Key == baseViewKey);
            if (view != null)
            {
                return view;
            }

            view = componentViews.FirstOrDefault(o => o.Key == baseViewKey);
            if (view != null)
            {
                return view;
            }

            return null;
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

        public IEnumerable<DynamicRelationshipView> DetectDynamicRelationshipsView(IEnumerable<RelationshipInViewToImport> relationshipsInView, IEnumerable<Relationship> existingRelationships)
        {
            var result = new List<DynamicRelationshipView>();
            foreach(var relationshipToImport in relationshipsInView)
            {
                var existingRelationship = existingRelationships
                    .FirstOrDefault(o => o.StructurizrId == relationshipToImport.StructurizrId);
                if(existingRelationship != null)
                {
                    var relationshipView = new DynamicRelationshipView();
                    relationshipView.Id = Guid.NewGuid().ToString();
                    relationshipView.Description = relationshipToImport.Description;
                    relationshipView.Response = relationshipToImport.Response;
                    relationshipView.Order = relationshipToImport.Order;
                    relationshipView.SourceId = relationshipView.Response.HasValue && relationshipView.Response.Value ? existingRelationship.DestinationId : existingRelationship.SourceId;
                    relationshipView.DestinationId = relationshipView.Response.HasValue && relationshipView.Response.Value ? existingRelationship.SourceId : existingRelationship.DestinationId;
                    relationshipView.BaseRelationshipId = existingRelationship.Id;
                    result.Add(relationshipView);
                }
            }
            return result;
        }


    }
}
