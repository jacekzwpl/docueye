using DocuEye.DocsKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Commads.SaveSingleDecision
{
    public class SaveSingleDecisionCommand : IRequest
    {
        public Decision Decision { get; set; }

        public SaveSingleDecisionCommand(Decision decision)
        {
            Decision = decision;
        }
    }
}
