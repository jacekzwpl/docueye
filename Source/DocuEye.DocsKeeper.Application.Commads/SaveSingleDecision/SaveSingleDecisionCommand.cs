using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Mediator.Commands;


namespace DocuEye.DocsKeeper.Application.Commads.SaveSingleDecision
{
    public class SaveSingleDecisionCommand : ICommand
    {
        public Decision Decision { get; set; }

        public SaveSingleDecisionCommand(Decision decision)
        {
            Decision = decision;
        }
    }
}
