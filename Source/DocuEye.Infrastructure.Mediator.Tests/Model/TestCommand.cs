using DocuEye.Infrastructure.Mediator.Commands;

public class TestCommand : ICommand<TestResult>
{
    public string Name { get; set; }

    public TestCommand(string name)
    {
        Name = name;
    }
}