﻿using DocuEye.ModelKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderModelElements : BaseExploderTests
    {
        [Test]
        public void WhenModelIsEmptyThenNoElementsAreExploded()
        {
            // Arrange
            var model = new StructurizrModel();
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements,relationships) = exloder.ExplodeModelElements(model);

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(0));
            Assert.That(relationships.Count(), Is.EqualTo(0));
        }

        [Test]
        public void WhenModelIsNullThenNoElementsAreExploded()
        {
            // Arrange
            StructurizrModel? model = null;
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeModelElements(model);

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(0));
            Assert.That(relationships.Count(), Is.EqualTo(0));
        }

        [Test]
        public void WhenModelElementsAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var model = new StructurizrModel
            {
                SoftwareSystems = new List<StructurizrSoftwareSystem>
                {
                    new StructurizrSoftwareSystem
                    {
                        Id = "1",
                        Name = "SoftwareSystem",
                        Relationships = new List<StructurizrRelationship>
                        {
                            new StructurizrRelationship
                            {
                                Id = "1",
                                SourceId = "1",
                                DestinationId = "2"
                            }
                        },
                        Containers = new List<StructurizrContainer>
                        {
                            new StructurizrContainer
                            {
                                Id = "4",
                                Name = "Container",
                                Relationships = new List<StructurizrRelationship>
                                {
                                    new StructurizrRelationship
                                    {
                                        Id = "2",
                                        SourceId = "4",
                                        DestinationId = "5"
                                    }
                                },
                                Components = new List<StructurizrComponent>
                                {
                                    new StructurizrComponent
                                    {
                                        Id = "5",
                                        Name = "Component"
                                    }
                                }
                            }
                        }
                    }
                },
                People = new List<StructurizrPerson>
                {
                    new StructurizrPerson
                    {
                        Id = "2",
                        Name = "Person",
                        Relationships = new List<StructurizrRelationship>
                        {
                            new StructurizrRelationship
                            {
                                Id = "3",
                                SourceId = "2",
                                DestinationId = "3"
                            }
                        }
                    }
                },
                DeploymentNodes = new List<StructurizrDeploymentNode>
                {
                    new StructurizrDeploymentNode
                    {
                        Id = "3",
                        Name = "DeploymentNode",
                        Relationships = new List<StructurizrRelationship>
                        {
                            new StructurizrRelationship
                            {
                                Id = "1",
                                SourceId = "1",
                                DestinationId = "2"
                            }
                        }
                        
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeModelElements(model);

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(5));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().Name, Is.EqualTo("SoftwareSystem"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.SoftwareSystem));
            Assert.That(elements.ElementAt(1).StructurizrId, Is.EqualTo("4"));
            Assert.That(elements.ElementAt(1).Name, Is.EqualTo("Container"));
            Assert.That(elements.ElementAt(1).Type, Is.EqualTo(ElementType.Container));
            Assert.That(elements.ElementAt(2).StructurizrId, Is.EqualTo("5"));
            Assert.That(elements.ElementAt(2).Name, Is.EqualTo("Component"));
            Assert.That(elements.ElementAt(2).Type, Is.EqualTo(ElementType.Component));
            Assert.That(elements.ElementAt(3).StructurizrId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(3).Name, Is.EqualTo("Person"));
            Assert.That(elements.ElementAt(3).Type, Is.EqualTo(ElementType.Person));
            Assert.That(elements.ElementAt(4).StructurizrId, Is.EqualTo("3"));
            Assert.That(elements.ElementAt(4).Name, Is.EqualTo("DeploymentNode"));
            Assert.That(elements.ElementAt(4).Type, Is.EqualTo(ElementType.DeploymentNode));

            Assert.That(relationships.Count(), Is.EqualTo(4));
        }
    }
}
