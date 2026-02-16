using System;

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

        public static string MapFromCliType(string type)
        {
            switch (type)
            {
                case "openapi":
                    return OpenApiDefinition;
                case "asyncapi":
                    return AsyncApiDefinition;
                default:
                    throw new ArgumentException($"Unsupported documentation file type {type}.");
            }
        }
    }
}
