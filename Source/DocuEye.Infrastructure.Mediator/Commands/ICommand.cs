namespace DocuEye.Infrastructure.Mediator.Commands
{
    public interface ICommand
    {
    }

    public interface ICommand<TResult> : ICommand
    {
    }

    
}
