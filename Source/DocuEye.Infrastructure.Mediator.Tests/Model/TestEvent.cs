using DocuEye.Infrastructure.Mediator.Events;

public class TestEvent : IEvent
{
    public string Name { get; set; }

    public TestEvent(string name)
    {
        Name = name;
    }
}