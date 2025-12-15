using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DocuEye.Infrastructure.Mediator
{
    public class MediatorOptions
    {
        IEnumerable<Assembly> _assemblies = Enumerable.Empty<Assembly>();
        public void UseAssembly(Assembly assembly)
        {
            _assemblies = _assemblies.Append(assembly);
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            return _assemblies;
        }
    }
}
