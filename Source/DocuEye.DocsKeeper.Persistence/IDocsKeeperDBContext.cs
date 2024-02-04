using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.MongoDB;
using System;

namespace DocuEye.DocsKeeper.Persistence
{
    /// <summary>
    /// MongoDB Context for DocsKeeper module
    /// </summary>
    public interface IDocsKeeperDBContext
    {
        /// <summary>
        /// Collection of images
        /// </summary>
        IGenericCollection<Image> Images { get; }
        /// <summary>
        /// Collection of documentations
        /// </summary>
        IGenericCollection<Documentation> Documentations { get; }
        /// <summary>
        /// Collection of decisions
        /// </summary>
        IGenericCollection<Decision> Decisions { get; }
        /// <summary>
        /// Collection of documentation files
        /// </summary>
        IGenericCollection<DocumentationFile> DocumentationFiles { get; }
    }
}
