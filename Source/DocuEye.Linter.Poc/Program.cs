// See https://aka.ms/new-console-template for more information
using DocuEye.Linter.Model;
using System.Linq.Dynamic.Core;

Console.WriteLine("Hello, World!");

var elements = new List<LinterModelElement>
{
    new LinterModelElement { Name = "testsystem", Tags = new List<string> { "SoftwareSystem", "Tag2" } },
    new LinterModelElement { Name = "anothersystem", Tags = new List<string> { "Container", "Tag3" } },
    new LinterModelElement { Name = "testsystem", Tags = new List<string> { "Tag3" } },
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
    }
};
foreach (var rule in rules)
{
    Console.WriteLine($"Rule: {rule.Name}, Expression: {rule.Expression}");
    var result = elements.AsQueryable().Where(rule.Expression, variables).Count();

    Console.WriteLine(result);
}
