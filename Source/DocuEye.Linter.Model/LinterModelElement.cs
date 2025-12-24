using System;
using System.Collections.Generic;

namespace DocuEye.Linter.Model
{
    public class LinterModelElement
    {
         public string Name { get; set; }
         public IEnumerable<string> Tags { get; set; }
         public string Technology { get; set; }
         
    }
}
