using DocuEye.Infrastructure.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Infrastructure.DataProtection
{
    public class DataProtectionKey : BaseEntity
    {
        public string Key { get; set; } = null!;
    }
}
