using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ViewChanges
{
    public class DetectFilteredViewsTests : BaseDetectorsTests
    {
        [Test]
        public void WhenFilteredViewIsDefindedWithIncludeAllFilterThenElmentsAndRelationshipsAreNotFiltered()
        {
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1",
                    ViewType = ViewType.FilteredView,
                    PaperSize = "A4",
                    ExternalBoundariesVisible = true,
                    StructurizrElementId = "1",
                    Title = "Title",
                    Tags = new List<string>() { "*" },
                    Description = "Description",
                    AutomaticLayout = new ViewAutomaticLayout()
                    {
                        RankDirection = "TopBottom"
                    },
                    BaseViewKey = "baseview",
                    Mode = "include",
                }
            };
            var viewsIdsMap = new Dictionary<string, string>()
            {
                { "view1", "viewid1" }
            };
            var systemLandscapeViews = new List<SystemLandscapeView>()
            {
                new SystemLandscapeView()
                {
                    Key = "baseview",
                    Elements = new List<ElementView>()
                    {
                        new ElementView()
                        {
                            Id = "1",
                            Name = "Element1"
                        },
                        new ElementView()
                        {
                            Id = "2",
                            Name = "Element2"
                        }
                    },
                    Relationships = new List<RelationshipView>()
                    {
                        new RelationshipView()
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2"
                        }
                    }
                }
            };
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var result = detector.DetectFilteredViews(
                "workspaceId", 
                viewsToImport, 
                viewsIdsMap, 
                systemLandscapeViews, 
                Enumerable.Empty<SystemContextView>(),
                Enumerable.Empty<ContainerView>(),
                Enumerable.Empty<ComponentView>()
                );

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo("viewid1"));
            Assert.That(result.First().WorkspaceId, Is.EqualTo("workspaceId"));
            Assert.That(result.First().Elements.Count, Is.EqualTo(2));
            Assert.That(result.First().Relationships.Count, Is.EqualTo(1));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().Title, Is.EqualTo("Title"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));
        }

        [Test]
        public void WhenFilteredViewIsDefindedWithIncludeTagsFilterThenElmentsAndRelationshipsAreFiltered()
        {
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1",
                    ViewType = ViewType.FilteredView,
                    PaperSize = "A4",
                    ExternalBoundariesVisible = true,
                    StructurizrElementId = "1",
                    Title = "Title",
                    Tags = new List<string>() { "test1" },
                    Description = "Description",
                    AutomaticLayout = new ViewAutomaticLayout()
                    {
                        RankDirection = "TopBottom"
                    },
                    BaseViewKey = "baseview",
                    Mode = "include",
                }
            };
            var viewsIdsMap = new Dictionary<string, string>()
            {
                { "view1", "viewid1" }
            };
            var systemLandscapeViews = new List<SystemLandscapeView>()
            {
                new SystemLandscapeView()
                {
                    Key = "baseview",
                    Elements = new List<ElementView>()
                    {
                        new ElementView()
                        {
                            Id = "1",
                            Name = "Element1",
                            Tags = new List<string>()
                            {
                                "test1"
                            }
                        },
                        new ElementView()
                        {
                            Id = "2",
                            Name = "Element2",
                            Tags = new List<string>()
                            {
                                "test2"
                            }
                        }
                    },
                    Relationships = new List<RelationshipView>()
                    {
                        new RelationshipView()
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Tags = new List<string>()
                            {
                                "test1"
                            }
                        },
                        new RelationshipView()
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Tags = new List<string>()
                            {
                                "test2"
                            }
                        }
                    }
                }
            };
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var result = detector.DetectFilteredViews(
                "workspaceId",
                viewsToImport,
                viewsIdsMap,
                systemLandscapeViews,
                Enumerable.Empty<SystemContextView>(),
                Enumerable.Empty<ContainerView>(),
                Enumerable.Empty<ComponentView>()
                );

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo("viewid1"));
            Assert.That(result.First().WorkspaceId, Is.EqualTo("workspaceId"));
            Assert.That(result.First().Elements.Count, Is.EqualTo(1));
            Assert.That(result.First().Relationships.Count, Is.EqualTo(1));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().Title, Is.EqualTo("Title"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));
        }

        [Test]
        public void WhenFilteredViewIsDefindedWithExcludeAllFilterThenElmentsAndRelationshipsAreFiltered()
        {
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1",
                    ViewType = ViewType.FilteredView,
                    PaperSize = "A4",
                    ExternalBoundariesVisible = true,
                    StructurizrElementId = "1",
                    Title = "Title",
                    Tags = new List<string>() { "*" },
                    Description = "Description",
                    AutomaticLayout = new ViewAutomaticLayout()
                    {
                        RankDirection = "TopBottom"
                    },
                    BaseViewKey = "baseview",
                    Mode = "exclude",
                }
            };
            var viewsIdsMap = new Dictionary<string, string>()
            {
                { "view1", "viewid1" }
            };
            var systemLandscapeViews = new List<SystemLandscapeView>()
            {
                new SystemLandscapeView()
                {
                    Key = "baseview",
                    Elements = new List<ElementView>()
                    {
                        new ElementView()
                        {
                            Id = "1",
                            Name = "Element1",
                            Tags = new List<string>()
                            {
                                "test1"
                            }
                        },
                        new ElementView()
                        {
                            Id = "2",
                            Name = "Element2",
                            Tags = new List<string>()
                            {
                                "test2"
                            }
                        }
                    },
                    Relationships = new List<RelationshipView>()
                    {
                        new RelationshipView()
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Tags = new List<string>()
                            {
                                "test1"
                            }
                        },
                        new RelationshipView()
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Tags = new List<string>()
                            {
                                "test2"
                            }
                        }
                    }
                }
            };
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var result = detector.DetectFilteredViews(
                "workspaceId",
                viewsToImport,
                viewsIdsMap,
                systemLandscapeViews,
                Enumerable.Empty<SystemContextView>(),
                Enumerable.Empty<ContainerView>(),
                Enumerable.Empty<ComponentView>()
                );

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo("viewid1"));
            Assert.That(result.First().WorkspaceId, Is.EqualTo("workspaceId"));
            Assert.That(result.First().Elements.Count, Is.EqualTo(0));
            Assert.That(result.First().Relationships.Count, Is.EqualTo(0));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().Title, Is.EqualTo("Title"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));
        }

        [Test]
        public void WhenFilteredViewIsDefindedWithExcludeTagsFilterThenElmentsAndRelationshipsAreFiltered()
        {
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1",
                    ViewType = ViewType.FilteredView,
                    PaperSize = "A4",
                    ExternalBoundariesVisible = true,
                    StructurizrElementId = "1",
                    Title = "Title",
                    Tags = new List<string>() { "test1" },
                    Description = "Description",
                    AutomaticLayout = new ViewAutomaticLayout()
                    {
                        RankDirection = "TopBottom"
                    },
                    BaseViewKey = "baseview",
                    Mode = "exclude",
                }
            };
            var viewsIdsMap = new Dictionary<string, string>()
            {
                { "view1", "viewid1" }
            };
            var systemLandscapeViews = new List<SystemLandscapeView>()
            {
                new SystemLandscapeView()
                {
                    Key = "baseview",
                    Elements = new List<ElementView>()
                    {
                        new ElementView()
                        {
                            Id = "1",
                            Name = "Element1",
                            Tags = new List<string>()
                            {
                                "test1"
                            }
                        },
                        new ElementView()
                        {
                            Id = "2",
                            Name = "Element2",
                            Tags = new List<string>()
                            {
                                "test2"
                            }
                        }
                    },
                    Relationships = new List<RelationshipView>()
                    {
                        new RelationshipView()
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Tags = new List<string>()
                            {
                                "test1"
                            }
                        },
                        new RelationshipView()
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Tags = new List<string>()
                            {
                                "test2"
                            }
                        }
                    }
                }
            };
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var result = detector.DetectFilteredViews(
                "workspaceId",
                viewsToImport,
                viewsIdsMap,
                systemLandscapeViews,
                Enumerable.Empty<SystemContextView>(),
                Enumerable.Empty<ContainerView>(),
                Enumerable.Empty<ComponentView>()
                );

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo("viewid1"));
            Assert.That(result.First().WorkspaceId, Is.EqualTo("workspaceId"));
            Assert.That(result.First().Elements.Count, Is.EqualTo(1));
            Assert.That(result.First().Relationships.Count, Is.EqualTo(1));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().Title, Is.EqualTo("Title"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));
        }

    }
}
