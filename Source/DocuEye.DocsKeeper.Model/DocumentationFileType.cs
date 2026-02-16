using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Model
{
    public static class DocumentationFileType
    {
        public const string OpenApiDefinition = "OpenApiDefinition";
        public const string AsyncApiDefinition = "AsyncApiDefinition";

        public static readonly string[] AllTypes = new string[]
        {
            OpenApiDefinition,
            AsyncApiDefinition
        };
    }
}
