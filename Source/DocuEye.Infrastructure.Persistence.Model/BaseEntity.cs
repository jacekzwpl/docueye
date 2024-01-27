using System;

namespace DocuEye.Infrastructure.Persistence.Model
{
    /// <summary>
    /// Base model entity
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Entity identifier
        /// </summary>
        public string Id { get; set; } = null!;
    }
}
