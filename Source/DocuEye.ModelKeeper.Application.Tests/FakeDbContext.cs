using DocuEye.Infrastructure.MongoDB;
using DocuEye.Infrastructure.Tests.FakeMongoDB;
using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;

namespace DocuEye.ViewsKeeper.Application.Tests
{
    public class FakeDbContext : IModelKeeperDBContext
    {
        private List<Element> elements { get; }
        private List<Relationship> relationships { get; }


        public IGenericCollection<Element> Elements
        {
            get
            {
                return new FakeGenericCollection<Element>(this.elements);
            }
        }

        public IGenericCollection<Relationship> Relationships
        {
            get
            {
                return new FakeGenericCollection<Relationship>(this.relationships);
            }
        }


        public FakeDbContext() { 
        
            this.elements = this.CreateElementsData();
            this.relationships = this.CreateRelationshipsData();

        }



        private List<Element> CreateElementsData()
        {
            return new List<Element>()
            {
                new Element()
                {
                    Id = "SoftwareSystemId1",
                    Name = "My System 1",
                    DslId = "DSL-SoftwareSystemId1",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.SoftwareSystem
                },
                new Element()
                {
                    Id = "ContainerId1",
                    Name = "My Container 1",
                    ParentId = "SoftwareSystemId1",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.Container
                },
                new Element()
                {
                    Id = "ContainerId2",
                    Name = "My Container 2",
                    ParentId = "SoftwareSystemId1",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.Container
                },
                new Element()
                {
                    Id = "DeploymentNodeId1",
                    Name = "My Deployment Node 1",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.DeploymentNode
                },
                new Element()
                {
                    Id = "DeploymentNodeId2",
                    Name = "My Deployment Node 2",
                    ParentId = "DeploymentNodeId1",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.DeploymentNode
                },
                new Element()
                {
                    Id = "DeploymentNodeId3",
                    Name = "My Deployment Node 3",
                    ParentId = "DeploymentNodeId1",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.DeploymentNode
                },
                new Element()
                {
                    Id = "SoftwareSystemId2",
                    Name = "My System 2",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.SoftwareSystem
                },
                new Element()
                {
                    Id = "ContainerId3",
                    Name = "My Container 3",
                    ParentId = "SoftwareSystemId2",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.Container
                },
                new Element()
                {
                    Id = "ContainerId4",
                    Name = "My Container 4",
                    ParentId = "SoftwareSystemId2",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.Container
                },
                new Element()
                {
                    Id = "ContainerId4Instance",
                    Name = "My Container 4",
                    ParentId = "DeploymentNodeId3",
                    WorkspaceId = "workspacetest1",
                    Type = ElementType.ContainerInstance
                },
            };
        }

        private List<Relationship> CreateRelationshipsData()
        {
            return new List<Relationship>()
            {
                new Relationship()
                {
                    Id = "RelationshipId1",
                    WorkspaceId = "workspacetest1",
                    SourceId = "ContainerId3",
                    DestinationId = "ContainerId1"
                },
                new Relationship()
                {
                    Id = "RelationshipId2",
                    WorkspaceId = "workspacetest1",
                    SourceId = "ContainerId2",
                    DestinationId = "ContainerId1"
                }
            };
        }

    }
}
