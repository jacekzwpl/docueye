﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrWorkspaceConfigurationUser
    {
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "ReadOnly";
    }
}
