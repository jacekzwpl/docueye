using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ChangeTracker.Model
{
    /// <summary>
    /// Model change Type
    /// </summary>
    public static class ModelChangeType
    {
        /// <summary>
        /// Means that element was created 
        /// </summary>
        public const string ElementCreated = "ElementCreated";
        /// <summary>
        /// Means that element has been changed
        /// </summary>
        public const string ElementUpdated = "ElementUpdated";
        /// <summary>
        /// Means that element has been deleted from the model
        /// </summary>
        public const string ElementDeleted = "ElementDeleted";
        /// <summary>
        /// Means that new relationship has been created
        /// </summary>
        public const string RelationshipCreated = "RelationshipCreated";
        /// <summary>
        /// Means that relationship has been changed
        /// </summary>
        public const string RelationshipUpdated = "RelationshipUpdated";
        /// <summary>
        /// Means that relationship has been deleted from the model
        /// </summary>
        public const string RelationshipDeleted = "RelationshipDeleted";
    }
}
