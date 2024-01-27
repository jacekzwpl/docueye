using DocuEye.Infrastructure.MongoDB;
using DocuEye.ModelKeeper.Model;

namespace DocuEye.ModelKeeper.Persistence
{
    /// <summary>
    /// MongoDB context for ModelKeeper module
    /// </summary>
    public interface IModelKeeperDBContext
    {
        /// <summary>
        /// Collection of Elements
        /// </summary>
        IGenericCollection<Element> Elements { get; }
        /// <summary>
        /// Collection of relationships
        /// </summary>
        IGenericCollection<Relationship> Relationships { get; }
    }
}
