using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Linter.Model
{
    public class LinterRule
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Severity { get; set; }
        public string Type { get; set; }
        public string? Expression { get; set; }
        public string? HelpLink { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
