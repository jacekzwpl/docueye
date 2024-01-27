using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Mappings;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.ViewsExploder;
using DocuEye.WorkspaceImporter.Model;
using MediatR;
using Moq;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public abstract class BaseWorkspaceChangeDetectorTests
    {
        protected IMapper mapper;
        protected IMediator mediator;
        protected WorkspaceImport import;

        protected List<Element> existingElements;
        protected List<ExplodedElement> explodedElements;
        protected List<Relationship> existingRelationships;
        protected List<ExplodedRelationship> explodedRelationships;

        protected ViewsExplodeResult explodedViewsResult;

        [SetUp]
        public void Setup()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<WorkspaceImporterFromExplodedMappingProfile>());
            mapper = config.CreateMapper();

            var mediator = new Mock<IMediator>();
            mediator.Setup(i => i.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            this.mediator = mediator.Object;

            this.import = new WorkspaceImport()
            {
                Id = Guid.NewGuid().ToString()
            };

            this.existingElements = this.CreateExistingElements();
            this.explodedElements = this.CreateExplodedElements();
            this.existingRelationships = this.CreateExistingRelationships();
            this.explodedRelationships = this.CreateExplodedRelationships();
            this.explodedViewsResult = this.CreateViewsExplodeResult();
        }

        private List<Element> CreateExistingElements()
        {
            return new List<Element>()
            {
                new Element()
                {
                    Id = "IdS1",
                    DslId = "S1",
                    Name = "System1",
                    Type = ElementType.SoftwareSystem,
                },
                new Element()
                {
                    Id = "IdS2",
                    DslId = "S2",
                    Name = "System2",
                    Type = ElementType.SoftwareSystem,
                },
                new Element()
                {
                    Id = "IdC1",
                    ParentId = "IdS1",
                    DslId = "C1",
                    Name = "Container1",
                    Type = ElementType.Container,
                },
                new Element()
                {
                    Id = "IdC2",
                    ParentId = "IdS1",
                    DslId = "C2",
                    Name = "Container2",
                    Type = ElementType.Container,
                },
                new Element()
                {
                    Id = "IdC01",
                    ParentId = "IdC1",
                    DslId = "C01",
                    Name = "Component1",
                    Type = ElementType.Component,
                },
                new Element()
                {
                    DslId = "P1",
                    Name = "Person1",
                    Type = ElementType.Person,
                },
                new Element()
                {
                    DslId = "P2",
                    Name = "Person2",
                    Type = ElementType.Person,
                },
                new Element()
                {
                    Id = "IdN1",
                    DslId = "N1",
                    Name = "Node1",
                    Type = ElementType.DeploymentNode,
                },
                new Element()
                {
                    Id = "IdN2",
                    DslId = "N2",
                    Name = "Node2",
                    Type = ElementType.DeploymentNode,
                },
                new Element()
                {
                    Id = "IdN1_1",
                    ParentId = "IdN1",
                    DslId = "N1_1",
                    Name = "Node1_1",
                    Type = ElementType.DeploymentNode,
                },
                new Element()
                {
                    Id = "IdSI1",
                    ParentId = "IdN2",
                    InstanceOfId = "IdS1",
                    DslId = "SI1",
                    Name = "System1",
                    Type = ElementType.SoftwareSystemInstance
                },
                new Element()
                {
                    Id = "IdCI1",
                    ParentId = "IdN1",
                    InstanceOfId = "IdC1",
                    DslId = "CI1",
                    Name = "Container1",
                    Type = ElementType.ContainerInstance
                }
            };
        }

        private List<ExplodedElement> CreateExplodedElements()
        {
            return new List<ExplodedElement>() {
                new ExplodedElement()
                {
                    StructurizrId = "S1",
                    DslId = "S1",
                    Name = "System1",
                    Type = ElementType.SoftwareSystem,
                },
                new ExplodedElement()
                {
                    StructurizrId = "S2",
                    DslId = "S2",
                    Name = "System2",
                    Type = ElementType.SoftwareSystem,
                },
                new ExplodedElement()
                {
                    StructurizrId = "C1",
                    StructurizrParentId = "S1",
                    DslId = "C1",
                    Name = "Container1",
                    Type = ElementType.Container,
                },
                new ExplodedElement()
                {
                    StructurizrId = "C2",
                    StructurizrParentId = "S1",
                    DslId = "C2",
                    Name = "Container2",
                    Type = ElementType.Container,
                },
                new ExplodedElement()
                {
                    StructurizrId = "C01",
                    StructurizrParentId = "C1",
                    DslId = "C01",
                    Name = "Component1",
                    Type = ElementType.Component,
                },
                new ExplodedElement()
                {
                    StructurizrId = "P1",
                    DslId = "P1",
                    Name = "Person1",
                    Type = ElementType.Person,
                },
                new ExplodedElement()
                {
                    StructurizrId = "P2",
                    DslId = "P2",
                    Name = "Person2",
                    Type = ElementType.Person,
                },
                new ExplodedElement()
                {
                    StructurizrId = "N1",
                    DslId = "N1",
                    Name = "Node1",
                    Type = ElementType.DeploymentNode,
                },
                new ExplodedElement()
                {
                    StructurizrId = "N2",
                    DslId = "N2",
                    Name = "Node2",
                    Type = ElementType.DeploymentNode,
                },
                new ExplodedElement()
                {
                    StructurizrId = "N1_1",
                    StructurizrParentId = "N1",
                    DslId = "N1_1",
                    Name = "Node1_1",
                    Type = ElementType.DeploymentNode,
                },
                new ExplodedElement()
                {
                    StructurizrId = "SI1",
                    StructurizrInstanceOfId = "S1",
                    StructurizrParentId = "N2",
                    DslId = "SI1",
                    Type = ElementType.SoftwareSystemInstance,
                },
                new ExplodedElement()
                {
                    StructurizrId = "CI1",
                    StructurizrInstanceOfId = "C1",
                    StructurizrParentId = "N1",
                    DslId = "CI1",
                    Type = ElementType.ContainerInstance,
                }

            };
        }

        private List<Relationship> CreateExistingRelationships()
        {
            return new List<Relationship>()
            {
                new Relationship()
                {
                    Id = "IdR1",
                    DslId = "R1",
                    Description = "DescriptionR1",
                    SourceId = "IdS1",
                    DestinationId = "IdS2"
                },
                new Relationship()
                {
                    Id = "IdR2",
                    DslId = "R2",
                    Description = "DescriptionR2",
                    SourceId = "IdC1",
                    DestinationId = "IdC2"
                }
            };
        }

        private List<ExplodedRelationship> CreateExplodedRelationships()
        {
            return new List<ExplodedRelationship>()
            {
                new ExplodedRelationship()
                {
                    StructurizrId = "R1",
                    Description = "DescriptionR1",
                    DslId = "R1",
                    StructurizrSourceId = "S1",
                    StructurizrDestinationId = "S2"
                },
                new ExplodedRelationship()
                {
                    StructurizrId = "R2",
                    Description = "DescriptionR2",
                    DslId = "R2",
                    StructurizrSourceId = "C1",
                    StructurizrDestinationId = "C2"
                }
            };
        }

        private ViewsExplodeResult CreateViewsExplodeResult()
        {
            return new ViewsExplodeResult()
            {
                LandscapeViews = new List<ExplodedSystemLandscapeView> {
                    new ExplodedSystemLandscapeView()
                },
                SystemContextViews = new List<ExplodedSystemContextView> {
                    new ExplodedSystemContextView()
                },
                ContainerViews = new List<ExplodedContainerView> {
                    new ExplodedContainerView()
                },
                ComponentViews = new List<ExplodedComponentView> {
                    new ExplodedComponentView()
                },
                DynamicViews = new List<ExplodedDynamicView> {
                    new ExplodedDynamicView()
                },
                DeploymentViews = new List<ExplodedDeploymentView> {
                    new ExplodedDeploymentView()
                },
                ImagesViews = new List<ExplodedImageView> {
                    new ExplodedImageView()
                }
            };
        }
    }
}
