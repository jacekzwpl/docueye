using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonModelMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonModel_To_LinterModelElement_Works()
        {
            // Arrange
            var source = new StructurizrJsonModel
            {
                SoftwareSystems = new[]
                {
                    new StructurizrJsonSoftwareSystem
                    {
                        Id = "ss1",
                        Name = "Software System 1",
                        Properties = new Dictionary<string, string>() {
                            { DslPropertyNames.DslIdProperty, "ss1-dsl-id" },
                        },
                        Tags = "Tag1,Tag2",
                        Containers = new[]
                        {
                            new StructurizrJsonContainer
                            {
                                Id = "c1",
                                Name = "Container 1",
                                Properties = new Dictionary<string, string>() {
                                    { DslPropertyNames.DslIdProperty, "c1-dsl-id" },
                                },
                                Tags = "Tag3,Tag4",
                                Components = new[]
                                {
                                    new StructurizrJsonComponent
                                    {
                                        Id = "comp1",
                                        Name = "Component 1",
                                        Properties = new Dictionary<string, string>() {
                                            { DslPropertyNames.DslIdProperty, "comp1-dsl-id" },
                                        },
                                        Tags = "Tag5,Tag6"
                                    }
                                }
                            }
                        }
                    }
                },
                People = new[]
                {
                    new StructurizrJsonPerson
                    {
                        Id = "p1",
                        Name = "Person 1",
                        Properties = new Dictionary<string, string>() {
                           { DslPropertyNames.DslIdProperty, "comp1-dsl-id" },
                        },
                        Tags = "Tag7,Tag8"
                    }
                },
                CustomElements = new[] {
                    new StructurizrJsonCustomElement
                    {
                        Id = "cust1",
                        Name = "Custom Element 1",
                        Properties = new Dictionary<string, string>() {
                           { DslPropertyNames.DslIdProperty, "cust1-dsl-id" },
                        },
                        Tags = "Tag7,Tag8"
                    }
                },
                DeploymentNodes = new[]
                {
                    new StructurizrJsonDeploymentNode
                    {
                        Id = "dn1",
                        Name = "Deployment Node 1",
                        Properties = new Dictionary<string, string>() {
                           { DslPropertyNames.DslIdProperty, "dn1-dsl-id" },
                        },
                        Tags = "Tag9,Tag10",
                        Children = new[]
                        {
                            new StructurizrJsonDeploymentNode
                            {
                                Id = "dn1-1",
                                Name = "Deployment Node 1.1",
                                Properties = new Dictionary<string, string>() {
                                   { DslPropertyNames.DslIdProperty, "dn1-1-dsl-id" },
                                },
                                Tags = "Tag11,Tag12",
                            }
                        },
                        InfrastructureNodes = new[]
                        {
                            new StructurizrJsonInfrastructureNode
                            {
                                Id = "in1",
                                Name = "Infrastructure Node 1",
                                Properties = new Dictionary<string, string>() {
                                   { DslPropertyNames.DslIdProperty, "in1-dsl-id" },
                                },
                                Tags = "Tag13,Tag14",
                            }
                        },
                        ContainerInstances = new[]
                        {
                            new StructurizrJsonContainerInstance
                            {
                                Id = "ci1",
                                Properties = new Dictionary<string, string>() {
                                   { DslPropertyNames.DslIdProperty, "ci1-dsl-id" },
                                },
                                ContainerId = "c1"
                            }
                        },
                        SoftwareSystemInstances = new[]
                        {
                            new StructurizrJsonSoftwareSystemInstance
                            {
                                Id = "ssi1",
                                Properties = new Dictionary<string, string>() {
                                   { DslPropertyNames.DslIdProperty, "ssi1-dsl-id" },
                                },
                                SoftwareSystemId = "ss1"
                            }
                        }
                    }
                }
            };
            // Act
            var elements = source.ToLinterModelElements();
            // Assert
            Assert.That(elements.Count(), Is.EqualTo(10));
            Assert.That(elements.Any(e => e.Identifier == "ss1-dsl-id"));
            Assert.That(elements.Any(e => e.Identifier == "c1-dsl-id"));
            Assert.That(elements.Any(e => e.Identifier == "comp1-dsl-id"));
            Assert.That(elements.Any(e => e.Identifier == "comp1-dsl-id"));
            Assert.That(elements.Any(e => e.Identifier == "dn1-dsl-id"));
            Assert.That(elements.Any(e => e.Identifier == "in1-dsl-id"));
            Assert.That(elements.Any(e => e.Identifier == "ci1-dsl-id"));
            Assert.That(elements.Any(e => e.Identifier == "ssi1-dsl-id"));
            Assert.That(elements.Any(e => e.Identifier == "dn1-1-dsl-id"));
            Assert.That(elements.Any(e => e.Identifier == "cust1-dsl-id"));

        }
    }
}
