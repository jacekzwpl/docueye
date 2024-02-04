using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.MongoDB;
using DocuEye.Infrastructure.Tests.FakeMongoDB;

namespace DocuEye.DocsKeeper.Application.Tests
{
    public class FakeDbContext : IDocsKeeperDBContext
    {

        private List<Image> images;
        private List<Documentation> documentations;
        private List<Decision> decisions;
        private List<DocumentationFile> documentationFiles;
        public FakeDbContext() { 
        
           this.decisions = this.CreateDecisions();
           this.documentations = this.CreateDocumentations();
           this.images = this.CreateImages();
        }

        public IGenericCollection<Image> Images
        {
            get
            {
                return new FakeGenericCollection<Image>(this.images);
            }
        }

        public IGenericCollection<Documentation> Documentations
        {
            get
            {
                return new FakeGenericCollection<Documentation>(this.documentations);
            }
        }

        public IGenericCollection<Decision> Decisions
        {
            get
            {
                return new FakeGenericCollection<Decision>(this.decisions);
            }
        }

        public IGenericCollection<DocumentationFile> DocumentationFiles
        {
            get
            {
                return new FakeGenericCollection<DocumentationFile>(this.documentationFiles);
            }
        }

        private List<Image> CreateImages()
        {
            return new List<Image>()
            {
                new Image()
                {
                    Id = "imageId1",
                    DocumentationId = "documentationId1",
                    WorkspaceId = "workspacetest1",
                    Name = "images/name.png",
                    Content = "www",
                    Type = "image/png"
                }
            };
        }

        private List<Documentation> CreateDocumentations ()
        {
            return new List<Documentation>()
            {
                new Documentation()
                {
                    Id = "documentationId1",
                    WorkspaceId = "workspacetest1",
                    Sections = new List<DocumentationSection>()
                    {
                        new DocumentationSection()
                        {
                            Content = "doc content 1"
                        },
                        new DocumentationSection()
                        {
                            Content = "doc content 2"
                        }
                    }
                },
                new Documentation()
                {
                    Id = "documentationId2",
                    WorkspaceId = "workspacetest1",
                    ElementId = "elementId1",
                    Sections = new List<DocumentationSection>()
                    {
                        new DocumentationSection()
                        {
                            Content = "doc content 1"
                        },
                        new DocumentationSection()
                        {
                            Content = "doc content 2"
                        }
                    }
                }
            };
        }

        private List<Decision> CreateDecisions()
        {
            return new List<Decision>()
            {
                new Decision()
                {
                    Id = "decisionId1",
                    WorkspaceId = "workspacetest1",
                    Content = "decision 1 content"
                },
                new Decision()
                {
                    Id = "decisionId2",
                    WorkspaceId = "workspacetest1",
                    Content = "decision 2 content"
                },
                new Decision()
                {
                    Id = "decisionId3",
                    WorkspaceId = "workspacetest1",
                    ElementId = "elementId1",
                    Content = "decision 1 content"
                },
                new Decision()
                {
                    Id = "decisionId4",
                    WorkspaceId = "workspacetest1",
                    ElementId = "elementId1",
                    Content = "decision 2 content"
                }
            };
        }
    }
}
