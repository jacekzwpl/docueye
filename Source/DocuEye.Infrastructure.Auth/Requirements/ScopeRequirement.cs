using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Auth.Requirements
{
    public class ScopeRequirement : IAuthorizationRequirement
    {
        public string ScopeName {  get; private set; }
        public bool IsEnabled { get; private set; }
        public ScopeRequirement(string scopeName, bool isEnabled) { 
            this.ScopeName = scopeName;
            this.IsEnabled = isEnabled;
        }
    }
}
