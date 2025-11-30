using DocuEye.Structurizr.DSL.Expressions;
using DocuEye.Structurizr.DSL.Expressions.Builders;
using DocuEye.Structurizr.DSL.Expressions.Model;
using DocuEye.Structurizr.DSL.Model;
using DocuEye.Structurizr.Json.Model;
using System.Linq.Expressions;

namespace DocuEye.Structurizr.DslToJson
{
    public partial class WorkspaceConverter
    {

        public StructurizrJsonViews ConvertViews(StructurizrViews dslViews)
        {
            var views = new StructurizrJsonViews();

            views.Configuration = this.ConvertViewsConfiguration(dslViews);

            if (dslViews.SystemLandscapeViews.Any())
            {
                var systemLandscapeViews = new List<StructurizrJsonSystemLandscapeView>();
                foreach (var dslView in dslViews.SystemLandscapeViews)
                {
                    var view = this.ConvertSystemLandscapeView(dslView);
                    systemLandscapeViews.Add(view);
                }

                views.SystemLandscapeViews = systemLandscapeViews.ToArray();
            }

            if (dslViews.SystemContextViews.Any())
            {
                var systemContextViews = new List<StructurizrJsonSystemContextView>();
                foreach (var dslView in dslViews.SystemContextViews)
                {
                    var view = this.ConvertSystemContextView(dslView);
                    systemContextViews.Add(view);
                }
                views.SystemContextViews = systemContextViews.ToArray();
            }

            if(dslViews.ContainerViews.Any())
            {
                var containerViews = new List<StructurizrJsonContainerView>();
                foreach (var dslView in dslViews.ContainerViews)
                {
                    var view = this.ConvertContainerView(dslView);
                    containerViews.Add(view);
                }
                views.ContainerViews = containerViews.ToArray();
            }

            if (dslViews.ComponentViews.Any())
            {
                var componentViews = new List<StructurizrJsonComponentView>();
                foreach (var dslView in dslViews.ComponentViews)
                {
                    var view = this.ConvertComponentView(dslView);
                    componentViews.Add(view);
                }
                views.ComponentViews = componentViews.ToArray();
            }

            if (dslViews.DeploymentViews.Any())
            {
                var deploymentViews = new List<StructurizrJsonDeploymentView>();
                foreach (var dslView in dslViews.DeploymentViews)
                {
                    var view = this.ConvertDeploymentView(dslView);
                    deploymentViews.Add(view);
                }
                views.DeploymentViews = deploymentViews.ToArray();
            }

            if (dslViews.DynamicViews.Any())
            {
                var dynamicViews = new List<StructurizrJsonDynamicView>();
                foreach (var dslView in dslViews.DynamicViews)
                {
                    var view = this.ConvertDynamicView(dslView);
                    dynamicViews.Add(view);
                }
                views.DynamicViews = dynamicViews.ToArray();
            }

            if (dslViews.FilteredViews.Any())
            {
                var filteredViews = new List<StructurizrJsonFilteredView>();
                foreach (var dslView in dslViews.FilteredViews)
                {
                    var view = this.ConvertFilteredView(dslView);
                    filteredViews.Add(view);
                }
                views.FilteredViews = filteredViews.ToArray();
            }

            if (dslViews.CustomViews.Any())
            {
                var customViews = new List<StructurizrJsonCustomView>();
                foreach (var dslView in dslViews.CustomViews)
                {
                    var view = this.ConvertCustomView(dslView);
                    customViews.Add(view);
                }
                views.CustomViews = customViews.ToArray();
            }

            if (dslViews.ImageViews.Any())
            {
                var imageViews = new List<StructurizrJsonImageView>();
                foreach (var dslView in dslViews.ImageViews)
                {
                    var view = this.ConvertImageView(dslView);
                    imageViews.Add(view);
                }
                views.ImageViews = imageViews.ToArray();
            }




            return views;
        }

        private (IEnumerable<StructurizrModelElement> elements, IEnumerable<StructurizrRelationship> relationships) DiscoverElementsAndRelationships(IEnumerable<string>? includes, IEnumerable<string>? excludes, Expression<Func<StructurizrModelElement, bool>> defaultElementsExpression)
        {

            var elements = new List<StructurizrModelElement>();
            var relationships = new List<StructurizrRelationship>();
            if (includes != null && includes.Count() == 1 && includes.First().Trim() == "*")
            {
                //If the include is "*", we include all elements with default filter by type
                elements = this.dslWorkspace.Model.Elements.AsQueryable()
                    .Where(defaultElementsExpression)
                    .ToList();
                relationships = this.dslWorkspace.Model.Relationships.ToList();
            }
            else if (includes != null && includes.Count() > 0)
            {
                var expressions = new List<DSLExpression>();
                foreach (var include in includes)
                {
                    if (include.Trim() == "*")
                    {
                        //If other filters are defined, we ignore the "*"
                        continue;
                    }
                    var parser = this.CreateExpressionsParserFromText(include);
                    var context = parser.generalExpression();
                    var visitor = new DSLExpressionsVisitor();
                    var result = visitor.VisitGeneralExpression(context);
                    expressions.AddRange(result);
                }

                var elementsExpressionBuilder = new ElementExpressionBuilder(this.dslWorkspace.Model.Relationships);
                var elementsExpression = elementsExpressionBuilder.BuildExpression(expressions);
                elements = this.dslWorkspace.Model.Elements.AsQueryable()
                    .Where(elementsExpression).ToList();
                var relationshipExpressionBuilder = new RelationshipExpressionBuilder();
                var relationshipsExpression = relationshipExpressionBuilder.BuildExpression(expressions);
                relationships = this.dslWorkspace.Model.Relationships.AsQueryable()
                    .Where(relationshipsExpression)
                    .ToList();

            }
            else
            {
                elements = new List<StructurizrModelElement>();
                relationships = new List<StructurizrRelationship>();
            }

            //Filter the elements by type allowed for landscape view
            elements = elements.AsQueryable()
                .Where(defaultElementsExpression)
                .ToList();

            if (excludes != null && excludes.Any())
            {
                var excludeAll = false;
                var expressions = new List<DSLExpression>();
                foreach (var exclude in excludes)
                {
                    if (exclude.Trim() == "*")
                    {
                        excludeAll = true;
                    }
                    var parser = this.CreateExpressionsParserFromText(exclude);
                    var context = parser.generalExpression();
                    var visitor = new DSLExpressionsVisitor();
                    var result = visitor.VisitGeneralExpression(context);
                    expressions.AddRange(result);
                }
                if (excludeAll)
                {

                    elements = new List<StructurizrModelElement>();
                    relationships = new List<StructurizrRelationship>();
                }
                else
                {

                    var expressionBuilder = new ElementExpressionBuilder(this.dslWorkspace.Model.Relationships);
                    if (expressionBuilder.AreAnyElementExpression(expressions))
                    {
                        var expression = expressionBuilder.BuildExpression(expressions);
                        var excludeIds = elements.AsQueryable().Where(expression).Select(o => o.ModelId).ToArray();
                        elements = elements.AsQueryable().Where(o => !excludeIds.Contains(o.ModelId)).ToList();
                    }

                    var relationshipExpressionBuilder = new RelationshipExpressionBuilder();
                    if (relationshipExpressionBuilder.AreAnyRelationshipExpressions(expressions))
                    {
                        var relationshipsExpression = relationshipExpressionBuilder.BuildExpression(expressions);
                        var relationshipsExcludeIds = relationships.AsQueryable()
                            .Where(relationshipsExpression)
                            .Select(o => o.ModelId).ToArray();
                        relationships = relationships.AsQueryable()
                            .Where(o => !relationshipsExcludeIds.Contains(o.ModelId))
                            .ToList();
                    }

                }

            }

            //Filter relationships if their source or destination is not included in view
            relationships = relationships
                .Where(o => elements.Any(e => e.Identifier == o.SourceIdentifier))
                .Where(o => elements.Any(e => e.Identifier == o.DestinationIdentifier))
                .ToList();

            return (elements, relationships);
        }

        private StructurizrJsonAutomaticLayout? ConvertAutomaticLayout(StructurizrAutoLayout? automaticLayout)
        {

            return automaticLayout == null ? null : new StructurizrJsonAutomaticLayout()
            {
                RankDirection = automaticLayout.RankDirection,
                NodeSeparation = automaticLayout.NodeSeparation,
                RankSeparation = automaticLayout.RankSeparation,
                Implementation = "Graphviz",
                EdgeSeparation = 0,
                Vertices = true

            };
        }

        public StructurizrJsonConfiguration ConvertViewsConfiguration(StructurizrViews dslViews)
        {
            return new StructurizrJsonConfiguration()
            {
                Styles = this.ConvertViewsConfigurationStyles(dslViews.Styles),
                Terminology = this.ConvertViewsConfigurationTerminology(dslViews.Terminology),
                Themes = dslViews.Themes?.ToArray() ?? Array.Empty<string>()
            };
        }

        private StructurizrJsonTerminology? ConvertViewsConfigurationTerminology(StructurizrTerminology? dslTerminology)
        {
            if (dslTerminology == null)
            {
                return null;
            }
            return new StructurizrJsonTerminology()
            {
                // Enterprise = dslTerminology?.Enterprise,
                Person = dslTerminology?.Person,
                SoftwareSystem = dslTerminology?.SoftwareSystem,
                Container = dslTerminology?.Container,
                Component = dslTerminology?.Component,
                // Code = dslTerminology?.Code,
                DeploymentNode = dslTerminology?.DeploymentNode,
                Relationship = dslTerminology?.Relationship,
                InfrastructureNode = dslTerminology?.InfrastructureNode
            };
        }


        public StructurizrJsonConfigurationStyles ConvertViewsConfigurationStyles(StructurizrStyles? dslStyles)
        {
            if (dslStyles == null)
            {
                return new StructurizrJsonConfigurationStyles();
            }

            return new StructurizrJsonConfigurationStyles()
            {
                Elements = dslStyles.ElementStyles?.Select(o => new StructurizrJsonElementStyle()
                {
                    Tag = o.Tag,
                    Background = o.Background,
                    Color = o.Color,
                    Shape = o.Shape,
                    Icon = o.Icon,
                    Width = o.Width,
                    Height = o.Height,
                    FontSize = o.FontSize,
                    Stroke = o.Stroke,
                    StrokeWidth = o.StrokeWidth == null ? null : o.StrokeWidth.ToString(),
                    Metadata = o.Metadata
                }).ToArray(),
                Relationships = dslStyles.RelationshipStyles?.Select(o => new StructurizrJsonRelationshipStyle()
                {
                    Tag = o.Tag,
                    Color = o.Color,
                    Routing = o.Routing,
                    Thickness = o.Thickness,
                    FontSize = o.FontSize,
                    //Dashed = o.Dashed
                }).ToArray()
            };
        }

        public StructurizrJsonSystemLandscapeView ConvertSystemLandscapeView(StructurizrSystemLandscapeView dslView)
        {

            var (elements, relationships) = this.DiscoverElementsAndRelationships(
                dslView.Include,
                dslView.Exclude,
                o => o.Type == StructurizrModelElementType.Person
                    || o.Type == StructurizrModelElementType.SoftwareSystem
                    || o.Type == StructurizrModelElementType.CustomElement);


            var view = new StructurizrJsonSystemLandscapeView()
            {
                Key = dslView.Identifier,
                Title = dslView.Title,
                Description = dslView.Description,
                AutomaticLayout = this.ConvertAutomaticLayout(dslView.AutomaticLayout),
                EnterpriseBoundaryVisible = true,
                Elements = elements.Select(o => new StructurizrJsonElementView()
                {
                    Id = o.ModelId,
                    X = 0,
                    Y = 0
                }).ToArray(),
                Relationships = relationships.Select(o => new StructurizrJsonRelationshipView()
                {
                    Id = o.ModelId
                }).ToArray()

            };

            return view;
        }

        public StructurizrJsonSystemContextView ConvertSystemContextView(StructurizrSystemContextView dslView)
        {
            //Discover all elements that are related to the element of the view
            var elementIds = new List<string>()
            {
                dslView.ElementIdentifier
            };
            elementIds.AddRange(
                this.dslWorkspace.Model.Relationships
                    .Where(o => o.SourceIdentifier == dslView.ElementIdentifier)
                    .Select(o => o.DestinationIdentifier).Distinct().ToArray()
            );
            elementIds.AddRange(
                this.dslWorkspace.Model.Relationships
                    .Where(o => o.DestinationIdentifier == dslView.ElementIdentifier)
                    .Select(o => o.SourceIdentifier).Distinct().ToArray()
            );

            var (elements, relationships) = this.DiscoverElementsAndRelationships(
                dslView.Include,
                dslView.Exclude,
                o => ((o.Type == StructurizrModelElementType.Person
                    || o.Type == StructurizrModelElementType.SoftwareSystem
                    || o.Type == StructurizrModelElementType.CustomElement)
                    ) && elementIds.Contains(o.Identifier));


            var view = new StructurizrJsonSystemContextView()
            {
                Key = dslView.Identifier,
                Title = dslView.Title,
                Description = dslView.Description,
                SoftwareSystemId = dslView.ElementIdentifier,
                EnterpriseBoundaryVisible = true,
                AutomaticLayout = this.ConvertAutomaticLayout(dslView.AutomaticLayout),
                Elements = elements.Select(o => new StructurizrJsonElementView()
                {
                    Id = o.ModelId,
                    X = 0,
                    Y = 0
                }).ToArray(),
                Relationships = relationships.Select(o => new StructurizrJsonRelationshipView()
                {
                    Id = o.ModelId
                }).ToArray(),
            };
            return view;
        }

        public StructurizrJsonContainerView ConvertContainerView(StructurizrContainerView dslView)
        {
            var elementIds = new List<string>();
            elementIds.AddRange(this.dslWorkspace.Model.Elements
                    .Where(o => o.ParentIdentifier == dslView.ElementIdentifier)
                    .Select(o => o.Identifier).Distinct().ToArray()
            );

            var sources = this.dslWorkspace.Model.Relationships
                .Where(o => elementIds.Contains(o.SourceIdentifier))
                .Select(o => o.DestinationIdentifier).Distinct().ToArray();

            var sourcesIds = this.dslWorkspace.Model.Elements
                .Where(o => sources.Contains(o.Identifier) && (o.Type == StructurizrModelElementType.Person
                    || o.Type == StructurizrModelElementType.SoftwareSystem
                    || o.Type == StructurizrModelElementType.CustomElement))
                .Select(o => o.Identifier).Distinct().ToArray();

            var destinations = this.dslWorkspace.Model.Relationships
                .Where(o => elementIds.Contains(o.DestinationIdentifier))
                .Select(o => o.SourceIdentifier).Distinct().ToArray();

            var destinationsIds = this.dslWorkspace.Model.Elements
                .Where(o => destinations.Contains(o.Identifier) && (o.Type == StructurizrModelElementType.Person
                    || o.Type == StructurizrModelElementType.SoftwareSystem
                    || o.Type == StructurizrModelElementType.CustomElement))
                .Select(o => o.Identifier).Distinct().ToArray();

            elementIds.AddRange(sourcesIds);
            elementIds.AddRange(destinationsIds);

            var (elements, relationships) = this.DiscoverElementsAndRelationships(
                dslView.Include,
                dslView.Exclude,
                o => ((o.Type == StructurizrModelElementType.Person
                    || o.Type == StructurizrModelElementType.SoftwareSystem
                    || o.Type == StructurizrModelElementType.CustomElement
                    || o.Type == StructurizrModelElementType.Container)
                    ) && elementIds.Contains(o.Identifier));

            var contextElement = this.dslWorkspace.Model.Elements
                .Where(o => o.Identifier == dslView.ElementIdentifier)
                .FirstOrDefault();
            return new StructurizrJsonContainerView()
            {
                Key = dslView.Identifier,
                Title = dslView.Title,
                Description = dslView.Description,
                SoftwareSystemId = contextElement?.ModelId,
                AutomaticLayout = this.ConvertAutomaticLayout(dslView.AutomaticLayout),
                Elements = elements.Select(o => new StructurizrJsonElementView()
                {
                    Id = o.ModelId,
                    X = 0,
                    Y = 0
                }).ToArray(),
                Relationships = relationships.Select(o => new StructurizrJsonRelationshipView()
                {
                    Id = o.ModelId
                }).ToArray()
            };
        }

        public StructurizrJsonComponentView ConvertComponentView(StructurizrComponentView dslView)
        {

            var elementIds = new List<string>();
            elementIds.AddRange(this.dslWorkspace.Model.Elements
                    .Where(o => o.ParentIdentifier == dslView.ElementIdentifier)
                    .Select(o => o.Identifier).Distinct().ToArray()
            );

            var sources = this.dslWorkspace.Model.Relationships
                .Where(o => elementIds.Contains(o.SourceIdentifier))
                .Select(o => o.DestinationIdentifier).Distinct().ToArray();

            var sourcesIds = this.dslWorkspace.Model.Elements
                .Where(o => sources.Contains(o.Identifier) && (
                       o.Type == StructurizrModelElementType.Person
                    || o.Type == StructurizrModelElementType.SoftwareSystem
                    || o.Type == StructurizrModelElementType.CustomElement
                    || o.Type == StructurizrModelElementType.Container))
                .Select(o => o.Identifier).Distinct().ToArray();

            var destinations = this.dslWorkspace.Model.Relationships
                .Where(o => elementIds.Contains(o.DestinationIdentifier))
                .Select(o => o.SourceIdentifier).Distinct().ToArray();

            var destinationsIds = this.dslWorkspace.Model.Elements
                .Where(o => destinations.Contains(o.Identifier) && (
                       o.Type == StructurizrModelElementType.Person
                    || o.Type == StructurizrModelElementType.SoftwareSystem
                    || o.Type == StructurizrModelElementType.CustomElement
                    || o.Type == StructurizrModelElementType.Container))
                .Select(o => o.Identifier).Distinct().ToArray();

            elementIds.AddRange(sourcesIds);
            elementIds.AddRange(destinationsIds);

            var (elements, relationships) = this.DiscoverElementsAndRelationships(
                dslView.Include,
                dslView.Exclude,
                o => ((o.Type == StructurizrModelElementType.Person
                    || o.Type == StructurizrModelElementType.SoftwareSystem
                    || o.Type == StructurizrModelElementType.CustomElement
                    || o.Type == StructurizrModelElementType.Container
                    || o.Type == StructurizrModelElementType.Component)
                    ) && elementIds.Contains(o.Identifier));

            return new StructurizrJsonComponentView()
            {
                Key = dslView.Identifier,
                Title = dslView.Title,
                Description = dslView.Description,
                ContainerId = dslView.ElementIdentifier,
                AutomaticLayout = this.ConvertAutomaticLayout(dslView.AutomaticLayout),
                Elements = elements.Select(o => new StructurizrJsonElementView()
                {
                    Id = o.ModelId,
                    X = 0,
                    Y = 0
                }).ToArray(),
                Relationships = relationships.Select(o => new StructurizrJsonRelationshipView()
                {
                    Id = o.ModelId
                }).ToArray()
            };
        }

        public StructurizrJsonFilteredView ConvertFilteredView(StructurizrFilteredView dslView)
        {

            return new StructurizrJsonFilteredView()
            {
                Key = dslView.Identifier,
                BaseViewKey = dslView.BaseViewIdentifier,
                Title = dslView.Title,
                Description = dslView.Description,
                Tags = dslView.Tags?.ToArray() ?? Array.Empty<string>(),
                Mode = dslView.FilterMode
            };
        }

        public StructurizrJsonDynamicView ConvertDynamicView(StructurizrDynamicView dslView)
        {

            var relationships = new List<StructurizrJsonRelationshipView>();

            foreach (var dynamicRelationship in dslView.Relationships)
            {
                StructurizrRelationship? relationship = null;
                var isResponse = false;
                relationship = this.dslWorkspace.Model.Relationships
                    .Where(o => o.SourceIdentifier == dynamicRelationship.Source
                        && o.DestinationIdentifier == dynamicRelationship.Destination).FirstOrDefault();
                if (relationship == null)
                {
                    relationship = this.dslWorkspace.Model.Relationships
                        .Where(o => o.DestinationIdentifier == dynamicRelationship.Source
                            && o.SourceIdentifier == dynamicRelationship.Destination).FirstOrDefault();
                    isResponse = true;
                }

                if (relationship != null)
                {
                    relationships.Add(new StructurizrJsonRelationshipView()
                    {
                        Id = relationship.ModelId,
                        Description = string.IsNullOrEmpty(dynamicRelationship.Description) ? relationship.Description : dynamicRelationship.Description,
                        Order = dynamicRelationship.Order.ToString(),
                        Response = isResponse
                    });
                }
            }

            var elementIds = dslView.Relationships.Select(o => o.Source)
                .Union(dslView.Relationships.Select(o => o.Destination))
                .Distinct()
                .ToArray();

            var elements = this.dslWorkspace.Model.Elements
                .Where(o => elementIds.Contains(o.Identifier))
                .Select(o => new StructurizrJsonElementView()
                {
                    Id = o.ModelId,
                    X = 0,
                    Y = 0
                }).ToArray();



            return new StructurizrJsonDynamicView()
            {
                Key = dslView.Identifier,
                Title = dslView.Title,
                Description = dslView.Description,
                ElementId = dslView.ElementIdentifier,
                Relationships = relationships.ToArray(),
                Elements = elements,
                ExternalBoundariesVisible = true,
                AutomaticLayout = this.ConvertAutomaticLayout(dslView.AutomaticLayout)
            };
        }

        public StructurizrJsonDeploymentView ConvertDeploymentView(StructurizrDeploymentView dslView)
        {

            var deploymentNodesIds = this.dslWorkspace.Model.Elements
                    .Where(o =>
                        o.Type == StructurizrModelElementType.DeploymentNode
                        && o.DeploymentEnvironmentIdentifier == dslView.Environment
                    ).Select(o => o.Identifier).Distinct().ToArray();

            var infrastructureNodesIds = this.dslWorkspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.InfrastructureNode
                    && o.DeploymentEnvironmentIdentifier == dslView.Environment)
                .Select(o => o.Identifier).Distinct().ToArray();

            var softwareSystemIds = this.dslWorkspace.Model.Elements
                .Where(o => o.Type == StructurizrModelElementType.SoftwareSystemInstance
                    && o.DeploymentEnvironmentIdentifier == dslView.Environment)
                .Select(o => o.Identifier).Distinct().ToArray();
            var containerInstancesIds = new List<string>();
            if (dslView.ElementIdentifier == "*")
            {
                containerInstancesIds.AddRange(
                    this.dslWorkspace.Model.Elements.Where(o =>
                        o.Type == StructurizrModelElementType.ContainerInstance
                        && o.DeploymentEnvironmentIdentifier == dslView.Environment)
                    .Select(o => o.Identifier).Distinct().ToArray()
               );
            }
            else
            {
                var containerIds = this.dslWorkspace.Model.Elements
                    .Where(o => o.Type == StructurizrModelElementType.Container
                        && o.ParentIdentifier == dslView.ElementIdentifier)
                    .Select(o => o.Identifier).Distinct().ToArray();
                containerInstancesIds.AddRange(
                    this.dslWorkspace.Model.Elements.Where(o =>
                        o.Type == StructurizrModelElementType.ContainerInstance
                        && o.DeploymentEnvironmentIdentifier == dslView.Environment
                        && containerIds.Contains(o.InstanceOfIdentifier))
                    .Select(o => o.Identifier).Distinct().ToArray()
                );
            }

            var elementIds = deploymentNodesIds
                .Union(infrastructureNodesIds)
                .Union(softwareSystemIds)
                .Union(containerInstancesIds)
                .Distinct()
                .ToArray();

            var (elements, relationships) = this.DiscoverElementsAndRelationships(
                dslView.Include,
                dslView.Exclude,
                o => ((o.Type == StructurizrModelElementType.DeploymentNode
                    || o.Type == StructurizrModelElementType.InfrastructureNode
                    || o.Type == StructurizrModelElementType.ContainerInstance
                    || o.Type == StructurizrModelElementType.SoftwareSystemInstance)
                    ) && elementIds.Contains(o.Identifier));


            return new StructurizrJsonDeploymentView()
            {
                AutomaticLayout = this.ConvertAutomaticLayout(dslView.AutomaticLayout),
                Key = dslView.Identifier,
                Title = dslView.Title,
                Description = dslView.Description,
                SoftwareSystemId = dslView.ElementIdentifier == "*" ? null : dslView.ElementIdentifier,
                Elements = elements.Select(o => new StructurizrJsonElementView()
                {
                    Id = o.ModelId,
                    X = 0,
                    Y = 0
                }).ToArray(),
                Relationships = relationships.Select(o => new StructurizrJsonRelationshipView()
                {
                    Id = o.ModelId
                }).ToArray()
            };
        }

        public StructurizrJsonCustomView ConvertCustomView(StructurizrCustomView dslView)
        {

            var (elements, relationships) = this.DiscoverElementsAndRelationships(
                dslView.Include,
                dslView.Exclude,
                o => (o.Type == StructurizrModelElementType.CustomElement)
             );
            return new StructurizrJsonCustomView()
            {
                Key = dslView.Identifier,
                Title = dslView.Title,
                Description = dslView.Description,
                Elements = elements.Select(o => new StructurizrJsonElementView()
                {
                    Id = o.ModelId,
                    X = 0,
                    Y = 0
                }).ToArray(),
                Relationships = relationships.Select(o => new StructurizrJsonRelationshipView()
                {
                    Id = o.ModelId
                }).ToArray()
            };
        }

        public StructurizrJsonImageView ConvertImageView(StructurizrImageView dslView)
        {
            var imageContentTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { ".jpg",  "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png",  "image/png" },
                { ".gif",  "image/gif" },
                { ".bmp",  "image/bmp" },
                { ".tiff", "image/tiff" },
                { ".tif",  "image/tiff" },
                { ".svg",  "image/svg+xml" },
                { ".webp", "image/webp" },
                { ".ico",  "image/x-icon" },
                { ".jfif", "image/jpeg" },
                { ".pjpeg", "image/pjpeg" },
                { ".pjp",  "image/pjpeg" }
            };

            string extension = GetImageExtension(dslView.ImageSource ?? string.Empty);
            string content = ImageToBase64Async(dslView.ImageSource ?? string.Empty, this.workspaceDirectory)
                .GetAwaiter().GetResult();
            string contentType = imageContentTypes.ContainsKey(extension) ? imageContentTypes[extension] : "application/octet-stream";
            return new StructurizrJsonImageView()
            {
                Key = dslView.Identifier,
                Title = dslView.Title,
                Description = dslView.Description,
                ElementId = dslView.ElementIdentifier,
                Content =  $"data:{contentType};base64,{content}",
                ContentType = contentType
            };
        }

        private static async Task<string> ImageToBase64Async(string pathOrUrl, string workspaceDirectory)
        {
            byte[] imageBytes;

            if (Uri.TryCreate(pathOrUrl, UriKind.Absolute, out var uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                using (var httpClient = new HttpClient())
                {
                    imageBytes = await httpClient.GetByteArrayAsync(pathOrUrl);
                }
            }
            else
            {
                imageBytes = await File.ReadAllBytesAsync(
                    Path.IsPathFullyQualified(pathOrUrl) ? 
                        pathOrUrl : 
                        Path.Combine(workspaceDirectory, pathOrUrl));
            }

            return System.Convert.ToBase64String(imageBytes);
        }

        private static string GetImageExtension(string pathOrUrl)
        {
            if (string.IsNullOrWhiteSpace(pathOrUrl))
                return string.Empty;

            var uri = new Uri(pathOrUrl, UriKind.RelativeOrAbsolute);

            string fileName = uri.IsAbsoluteUri ? Path.GetFileName(uri.LocalPath) : Path.GetFileName(pathOrUrl);
            return Path.GetExtension(fileName)?.ToLowerInvariant() ?? string.Empty;
        }
    }
}
