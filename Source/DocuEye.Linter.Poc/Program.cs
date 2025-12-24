// See https://aka.ms/new-console-template for more information
using DocuEye.Structurizr.DSL.Model;
using System.Linq;
using System.Linq.Dynamic.Core;

Console.WriteLine("Hello, World!");


var workspace = new StructurizrWorkspace();

workspace.Model = new StructurizrModel();

workspace.Model.AddModelElement(new StructurizrSoftwareSystem("testsystem")
{
    Name = "testsystem",
    Description = "A test system",
    Tags = new List<string> { "Tag1", "Tag2" }
});
workspace.Model.AddModelElement(new StructurizrContainer("testperson", "testsystem")
{
    Description = "A test container",
    Tags = new List<string> { "Tag1", "Tag3" }
});

var result = workspace.Model.Elements.AsQueryable().Where("Name == \"testsystem\" AND Tags.Contains(\"Tag1\")").Count();

Console.WriteLine(result);