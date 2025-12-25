using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Linter.Model
{
    public class LinterModel
    {
        public IEnumerable<LinterModelElement> Elements { get; set; } = null!;
        public IEnumerable<LinterModelRelationship> Relationships { get; set; } = null!;
    }
}
