using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Linter.Model
{

    public static class LinterRuleSeverity
    {
        public const string Error = "Error";
        public const string Warning = "Warning";
        public const string Information = "Information";

        public static int GetSeverityValue(string severity) 
        {
            return severity switch
            {
                Error => 3,
                Warning => 2,
                Information => 1,
                _ => 0,
            };
        }
    }
}
