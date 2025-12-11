using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Mediator.Tests.Handlers
{
    public class EventHandlingState
    {
        public List<string> HandledEvents { get; } = new List<string>();
    }
}
