using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Linter.Model
{
    public class LinterRule
    {
        public string Key { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Severity { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Expression { get; set; } = null!;
        public string? HelpLink { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
