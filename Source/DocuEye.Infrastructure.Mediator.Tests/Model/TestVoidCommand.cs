using DocuEye.Infrastructure.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Mediator.Tests.Model
{
    public class TestVoidCommand : ICommand
    {
        public string Name { get; set; }

        public TestVoidCommand(string name)
        {
            Name = name;
        }
    }
}
