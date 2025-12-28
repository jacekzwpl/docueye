using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.CustomTypeProviders;
using System.Text;
using System.Xml.Linq;

namespace DocuEye.Linter.Poc
{
    public class MyCustomTypeProvider : DefaultDynamicLinqCustomTypeProvider
    {
        public override HashSet<Type> GetCustomTypes() =>
            new[] { typeof(IEnumerable<LinterModelRelationship>),}.ToHashSet();
    }
}
