using AutoMapper;
using DocuEye.Structurizr.Model;
using DocuEye.WorkspaceImporter.Application.Mappings;

namespace DocuEye.WorkspaceImporter.Application.Tests.ModelExploder
{
    public class BaseModelExploderTests 
    {
        protected IMapper mapper;
        protected StructurizrModel model;

        [SetUp]
        public void Setup()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => 
            cfg.AddProfile<WorkspaceImporterFromStructurizrMappingProfile>());
            mapper = config.CreateMapper();


            this.model = new StructurizrModel()
            {
                SoftwareSystems = new List<StructurizrSoftwareSystem>()
                {
                    new StructurizrSoftwareSystem()
                    {
                        Id = "S1",
                        Name = "System1",
                        Containers = new List<StructurizrContainer>() {
                            new StructurizrContainer() {
                                Id = "S1.C1",
                                Name = "Container1",
                                Url = "https://docueye.com",
                                Components = new List<StructurizrComponent>()
                                {
                                    new StructurizrComponent()
                                    {
                                        Id = "S1.C1.C1",
                                        Name = "Component1",
                                        Relationships = new List<StructurizrRelationship>()
                                        {
                                            new StructurizrRelationship()
                                            {
                                                Id = "R5",
                                                Description = "R5Description",
                                                SourceId = "S1.C1.C1",
                                                DestinationId = "S1"
                                            }
                                        }
                                    }
                                },
                                Relationships = new List<StructurizrRelationship>()
                                {
                                    new StructurizrRelationship()
                                    {
                                        Id = "R4",
                                        Description = "R4Description",
                                        SourceId = "S1.C1",
                                        DestinationId = "S1"
                                    }
                                }
                            }

                        },
                        Relationships = new List<StructurizrRelationship>()
                        {
                            new StructurizrRelationship()
                            {
                                Id = "R1",
                                Description = "R1Description",
                                SourceId = "S1",
                                DestinationId = "S1.C1"
                            }
                        }

                    }
                },
                People = new List<StructurizrPerson>()
                {
                    new StructurizrPerson()
                    {
                        Id = "P1",
                        Name = "Person1",
                        Relationships = new List<StructurizrRelationship>()
                        {
                            new StructurizrRelationship()
                            {
                                Id = "R2",
                                Description = "R2Description",
                                SourceId = "P1",
                                DestinationId = "S1"
                            }
                        }
                    }
                },
                DeploymentNodes = new List<StructurizrDeploymentNode>() {
                    new StructurizrDeploymentNode()
                    {
                        Id = "DN1",
                        Name = "DeploymentNode1",
                        Children = new List<StructurizrDeploymentNode>()
                        {
                            new StructurizrDeploymentNode()
                            {
                                Id = "DN2",
                                Name = "DeploymentNode2",
                                ContainerInstances = new List<StructurizrContainerInstance>() {
                                    new StructurizrContainerInstance()
                                    {
                                        Id = "CI1",
                                        ContainerId = "S1.C1"
                                    }
                                }
                            }
                        },
                        Relationships = new List<StructurizrRelationship>()
                        {
                            new StructurizrRelationship()
                            {
                                Id = "R3",
                                Description = "R2Description",
                                SourceId = "DN1",
                                DestinationId = "DN3"
                            }
                        }
                    },
                    new StructurizrDeploymentNode()
                    {
                        Id = "DN3",
                        Name = "DeploymentNode3",
                        SoftwareSystemInstances = new List<StructurizrSoftwareSystemInstance>() {
                            new StructurizrSoftwareSystemInstance()
                            {
                                Id = "SSI1",
                                SoftwareSystemId = "S1"
                            }
                        }
                    }
                }
            };

        }
    }
}
