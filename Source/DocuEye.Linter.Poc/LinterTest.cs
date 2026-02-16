using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core.CustomTypeProviders;
using System.Text;

namespace DocuEye.Linter.Poc
{
    [DynamicLinqType]
    public static class LinterTest
    {
        public static bool HasCycle(LinterModelRelationship relationship, IEnumerable<LinterModelRelationship> allRelationships) { 
            return true;
        }
    }
}
