using DocuEye.Infrastructure.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Infrastructure.DataProtection
{
    /// <summary>
    /// Data Protection key stored in DB
    /// </summary>
    public class DataProtectionKey : BaseEntity
    {
        /// <summary>
        /// Data Protection Key
        /// </summary>
        public string Key { get; set; } = null!;
        /// <summary>
        /// Data Protection Key Frendly Name
        /// </summary>
        public string FrendlyName { get; set; } = null!;
    }
}
