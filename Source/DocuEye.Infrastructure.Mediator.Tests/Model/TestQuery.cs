using DocuEye.Infrastructure.Mediator.Queries;

public class TestQuery : IQuery<TestResult>
{
    public string Name { get; set; }

    public TestQuery(string name)
    {
        Name = name;
    }
}