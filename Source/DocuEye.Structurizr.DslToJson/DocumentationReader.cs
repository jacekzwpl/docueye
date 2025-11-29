using DocuEye.Structurizr.Json.Model;
using HeyRed.Mime;
using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System.Xml.Linq;

namespace DocuEye.Structurizr.DslToJson
{
    public class DocumentationReader
    {
        private string workspaceDirectory;

        public DocumentationReader(string workspaceDirectory)
        {
            this.workspaceDirectory = workspaceDirectory;
        }

        public StructurizrJsonDocumentation? Read(string? docsPath, string? adrPath)
        {
            var documentation = new StructurizrJsonDocumentation();

            documentation.Sections = string.IsNullOrEmpty(docsPath) ? null : this.ReadSections(docsPath);
            documentation.Decisions = string.IsNullOrEmpty(adrPath) ? null : this.ReadDecisions(adrPath);
            var images = new List<StructurizrJsonImage>();
            if (documentation.Sections != null)
            {
                var basePath = File.Exists(docsPath) ? Path.GetDirectoryName(docsPath) : docsPath;
                foreach (var section in documentation.Sections)
                { 
                    var image = this.DiscoverImage(basePath ?? this.workspaceDirectory, section.Content ?? "");
                    if (image != null && image.Count() > 0)
                    {
                        images.AddRange(image);
                    }
                }
            }

            if (documentation.Decisions != null)
            {
                var basePath = File.Exists(adrPath) ? Path.GetDirectoryName(adrPath) : adrPath;
                foreach (var decision in documentation.Decisions)
                {
                    var image = this.DiscoverImage(basePath ?? this.workspaceDirectory, decision.Content ?? "");
                    if (image != null && image.Count() > 0)
                    {
                        images.AddRange(image);
                    }
                }
            }

            documentation.Images = images.Count() > 0 ? images : null;

            if (documentation.Sections == null && documentation.Decisions == null && documentation.Images == null)
            {
                return null;
            }
            return documentation;
        }

        public IEnumerable<StructurizrJsonImage> DiscoverImage(string basePath, string markdownCpntent)
        {
            var pipeline = new MarkdownPipelineBuilder().Build();
            var document = Markdown.Parse(markdownCpntent, pipeline);
            var images = new List<StructurizrJsonImage>();
            foreach (var block in document.Descendants())
            {
                if (block is LinkInline link && link.IsImage)
                {
                    if(link.Url != null && !link.Url.StartsWith("http"))
                    {
                        string path = link.Url;
                        if (!Path.IsPathFullyQualified(link.Url))
                        {
                            path = Path.Combine(basePath, link.Url);
                        }
                        var image = new StructurizrJsonImage()
                        {
                            Content = File.ReadAllText(path),
                            Name = link.Url,
                            Type = MimeTypesMap.GetMimeType(path)
                        };
                        images.Add(image);
                    }
                }
            }
            return images;
        }

        

        public IEnumerable<StructurizrJsonDocumentationSection> ReadSections(string path)
        {
            if (!Path.IsPathFullyQualified(path))
            {
                path = Path.Combine(this.workspaceDirectory, path);
            }


            if (File.Exists(path))
            {
                var section = this.ReadSection(path);
                section.Order = 1;
                return new StructurizrJsonDocumentationSection[] {
                    section
                };
            }

            if (Directory.Exists(path))
            {
                var sections = new List<StructurizrJsonDocumentationSection>();
                Directory.GetFiles(path, "*.md", SearchOption.AllDirectories)
                    .ToList()
                    .ForEach(file =>
                    {
                        var section = this.ReadSection(file);
                        section.Order = sections.Count() + 1;
                        sections.Add(section);
                    });
                return sections.ToArray();
            }


            return new StructurizrJsonDocumentationSection[] { };
        }

        public StructurizrJsonDocumentationSection ReadSection(string path)
        {
            if (!Path.IsPathFullyQualified(path))
            {
                path = Path.Combine(this.workspaceDirectory, path);
            }
            var section = new StructurizrJsonDocumentationSection()
            {
                Content = File.ReadAllText(path),
                Format = "Markdown",
                Order = 0
            };
            return section;
        }

        public IEnumerable<StructurizrJsonDecision> ReadDecisions(string path)
        {
            if (!Path.IsPathFullyQualified(path))
            {
                path = Path.Combine(this.workspaceDirectory, path);
            }

            if (File.Exists(path))
            {
                var decision = this.ReadDecision(path);
                decision.Id = "1";
                return new StructurizrJsonDecision[] { decision };
            }

            if (Directory.Exists(path))
            {
                var decisions = new List<StructurizrJsonDecision>();
                Directory.GetFiles(path, "*.md", SearchOption.AllDirectories)
                    .ToList()
                    .ForEach(file =>
                    {
                        var decision = this.ReadDecision(file);
                        decision.Id = (decisions.Count() + 1).ToString();
                        decisions.Add(decision);
                    });
                return decisions.ToArray();
            }

            return new StructurizrJsonDecision[] { };
        }

        public StructurizrJsonDecision ReadDecision(string path)
        {
            if (!Path.IsPathFullyQualified(path))
            {
                path = Path.Combine(this.workspaceDirectory, path);
            }

            var markdownContent = File.ReadAllText(path);
            var pipeline = new MarkdownPipelineBuilder().Build();
            var document = Markdown.Parse(markdownContent, pipeline);

            string currentHeading = string.Empty;
            string status = string.Empty;
            DateTime date = DateTime.Now.Date;
            string title = string.Empty;
            foreach (var block in document)
            {
                if (block is HeadingBlock headingBlock)
                {
                    if (headingBlock.Level == 1 && currentHeading == string.Empty)
                    {
                        title = headingBlock.Inline?.FirstChild?.ToString() ?? string.Empty;
                        currentHeading = "Title";
                    }
                    else if (headingBlock.Level == 2 && headingBlock.Inline?.FirstChild?.ToString()?.ToLower().Trim() == "status")
                    {
                        currentHeading = "Status";
                    }
                }

                if (currentHeading == "Title" && block is ParagraphBlock titleParagraph)
                {
                    var dateStr = titleParagraph.Inline?.FirstChild?.ToString() ?? string.Empty;
                    dateStr = dateStr.Trim().Substring(5).Trim();
                    if (DateTime.TryParse(dateStr, out DateTime dateValue))
                    {
                        date = dateValue;
                    }
                }

                if (currentHeading == "Status" && block is ParagraphBlock statusParagraph)
                {
                    status = statusParagraph.Inline?.FirstChild?.ToString() ?? string.Empty;
                    break;
                }
            }


            var decision = new StructurizrJsonDecision()
            {
                Content = File.ReadAllText(path),
                Format = "Markdown",
                Date = date.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Title = title,
                Status = status,
            };
            return decision;
        }
    }
}