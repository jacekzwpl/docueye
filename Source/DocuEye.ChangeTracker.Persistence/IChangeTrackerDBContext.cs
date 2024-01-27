using DocuEye.ChangeTracker.Model;
using DocuEye.Infrastructure.MongoDB;
using System;

namespace DocuEye.ChangeTracker.Persistence
{
    /// <summary>
    /// MongoDB Context for ChangeTracker module
    /// </summary>
    public interface IChangeTrackerDBContext
    {
        /// <summary>
        /// Collection of model changes
        /// </summary>
        IGenericCollection<ModelChange> ModelChanges { get; }
    }
}
