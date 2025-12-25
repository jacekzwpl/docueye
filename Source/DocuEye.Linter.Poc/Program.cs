// See https://aka.ms/new-console-template for more information
using DocuEye.Linter.Model;
using System.Linq.Dynamic.Core;

Console.WriteLine("Hello, World!");

var elements = new List<LinterModelElement>
{
    new LinterModelElement { 
        Identifier = "mysystem",
        Name = "My System", 
        Tags = new List<string> { "SoftwareSystem" } 
    },
    new LinterModelElement { 
        Identifier = "mysystem.api",
        ParentIdentifier = "mysystem",
        Name = "My api", 
        Tags = new List<string> { "Container", "Tag3" } 
    },
    new LinterModelElement { 
        Identifier = "mysystem.frontend",
        ParentIdentifier = "mysystem",
        Name = "My frontend", 
        Tags = new List<string> { "Container", "Frontend" } 
    },
    new LinterModelElement { 
        Identifier = "mysystem.database",
        ParentIdentifier = "mysystem",
        Name = "My database", 
        Tags = new List<string> { "Container", "Database" } 
    },
    new LinterModelElement { 
        Identifier = "externalsystem",
        Name = "My External System",
        Tags = new List<string> { "SoftwareSystem" }
    },
    new LinterModelElement {
        Identifier = "externalsystem.service",
        ParentIdentifier = "externalsystem",
        Name = "External Service",
        Tags = new List<string> { "Container", "TagX" }
    }
};

var relationships = new List<LinterModelRelationship>
{
    new LinterModelRelationship
    {
        Source = elements.First(o => o.Identifier=="mysystem.frontend"),
        Destination = elements.First(o => o.Identifier=="mysystem.database")
    },
    new LinterModelRelationship
    {
        Source = elements.First(o => o.Identifier=="externalsystem.service"),
        Destination = elements.First(o => o.Identifier=="mysystem.database")
    },
    new LinterModelRelationship
    {
        Source = elements.First(o => o.Identifier=="externalsystem.service"),
        Destination = elements.First(o => o.Identifier=="mysystem.api")
    },
    new LinterModelRelationship
    {
        Source = elements.First(o => o.Identifier=="mysystem.api"),
        Destination = elements.First(o => o.Identifier=="externalsystem.service")
    }
};

Dictionary<string, object> variables = new Dictionary<string, object>
{
    { "AllowedTechnologies", new[] { "REST" } }
};

var rules = new List<LinterRule>
{
    new LinterRule
    {
        Key =  "ContainerTechnologyMustBeDefined",
        Name = "Container technology must be defined",
        Description = "Each container must have a defined technology to ensure clarity in the architecture.",
        Severity = "Warning",
        Type = "Element",
        Expression = "Tags.Contains(\"Container\") and Technology == null",
        HelpLink = "https://example.com/rules/container-technology-must-be-defined"
    },
    new LinterRule
    {
        Key = "ContainerAllowedTechnologies",
        Name = "Container technology must be one of the allowed technologies",
        Description = "Containers should use only approved technologies to maintain consistency and supportability.",
        Severity = "Warning",
        Type = "Element",
        Expression = "Tags.Contains(\"Container\") and Technology not_in (\"REST\")",
        HelpLink = "https://example.com/rules/container-allowed-technologies"
    },
    new LinterRule
    {
        Key = "FrontendDbConnectionNotAllowed",
        Name = "Frontend can not connect directly to database",
        Description = "Frontend services should not connect directly to the database for security and architecture reasons.",
        Severity = "Error",
        Type = "Relationship",
        Expression = "Source.Tags.Contains(\"Frontend\") and Destination.Tags.Contains(\"Database\")",
        HelpLink = "https://example.com/rules/no-direct-database-access-from-frontend"
    },
    new LinterRule
    {
        Key = "RelationshipsFromExternalSystemsToDatabaseNotAllowed",
        Name = "Relationships from external systems to database are not allowed",
        Description = "External systems should not have direct relationships to databases to ensure security and proper architecture.",
        Severity = "Error",
        Type = "Relationship",
        Expression = "Source.ParentIdentifier != Destination.ParentIdentifier and Destination.Tags.Contains(\"Database\")",
        HelpLink = "https://example.com/rules/no-direct-database-access-from-frontend"
    },
    new LinterRule
    {
        Key = "RelationshipTechnologyMustBeDefined",
        Name = "Relationship technology must be defined",
        Description = "Each relationship must have a defined technology to ensure clarity in the architecture.",
        Severity = "Warning",
        Type = "Relationship",
        Expression = "Technology == null",
        HelpLink = "https://example.com/rules/relationship-technology-must-be-defined"
    }
};

foreach (var rule in rules.Where(o => o.Type =="Element"))
{
    Console.WriteLine($"Rule: {rule.Name}, Expression: {rule.Expression}");
    var result = elements.AsQueryable().Where(rule.Expression, variables).Count();
    
    Console.WriteLine(result);
}

foreach (var rule in rules.Where(o => o.Type =="Relationship"))
{
    Console.WriteLine($"Rule: {rule.Name}, Expression: {rule.Expression}");
    var result = relationships.AsQueryable().Where(rule.Expression, variables).Count();
    Console.WriteLine(result);
}
Console.WriteLine("ContainersCyclingDependency");
foreach (var relationship in relationships.Where(o => o.Source.Tags.Contains("Container") && o.Destination.Tags.Contains("Container")))
{
    var result = relationships
        .Where (o => 
            o.Source.Identifier == relationship.Destination.Identifier 
            && o.Destination.Identifier == relationship.Source.Identifier
            && o.Source.Tags.Contains("Container")
            && o.Destination.Tags.Contains("Container")
            ).Count();
    if (result > 0) { 
        Console.WriteLine($"Found cycling dependency {relationship.Source.Name} <-> {relationship.Destination.Name}"); 
    }
}
