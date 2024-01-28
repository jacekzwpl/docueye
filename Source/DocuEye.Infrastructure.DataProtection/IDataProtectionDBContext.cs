using DocuEye.Infrastructure.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Infrastructure.DataProtection
{
    /// <summary>
    /// MongoDB context for Data Protection
    /// </summary>
    public interface IDataProtectionDBContext
    {
        /// <summary>
        /// Collection of all Data Protection Keys
        /// </summary>
        IGenericCollection<DataProtectionKey> DataProtectionKeys { get; }
    }
}
