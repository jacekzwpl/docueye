using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.IssueTracker.Application.Commands.SaveIssues;
using DocuEye.IssueTracker.Model;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation;
using DocuEye.WorkspaceImporter.Application.Commands.ImportElements;
using DocuEye.WorkspaceImporter.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportIssues
{
    public class ImportIssuesCommandHandler : BaseImportDataCommandHandler, ICommandHandler<ImportIssuesCommand, ImportIssuesResult>
    {
        private readonly IMediator mediator;

        public ImportIssuesCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<ImportIssuesResult> HandleAsync(ImportIssuesCommand command, CancellationToken cancellationToken = default)
        {
            // Check import data
            var import = await this.GetImport(command.WorkspaceId, command.ImportKey);
            var checkImport = this.CheckImport(import, command.WorkspaceId, command.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new ImportIssuesResult(
                        command.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            var saveIssuesCommand = new SaveIssuesCommand
            {
                WorkspaceId = command.WorkspaceId,
                Issues = command.Issues.Select(o => new Issue
                {
                    Id = Guid.NewGuid().ToString(),
                    Element = o.Element == null ? null : new IssueElement
                    {
                       DslId = o.Element.Identifier,
                       Name = o.Element.Name
                    },
                    Message = o.Message,
                    Severity = o.SeverityValue,
                    WorkspaceId = command.WorkspaceId,
                    Relationship = o.Relationship == null ? null : new IssueRelationship
                    {
                        Source = new IssueElement
                        {
                            DslId = o.Relationship.Source.Identifier,
                            Name = o.Relationship.Source.Name
                        },
                        Destination = new IssueElement
                        {
                            DslId = o.Relationship.Destination.Identifier,
                            Name = o.Relationship.Destination.Name
                        }
                    },
                    Rule = new IssueRule
                    {
                        Id = o.Rule.Id,
                        Name = o.Rule.Name,
                        Description = o.Rule.Description,
                        HelpLink = o.Rule.HelpLink
                    },
                }).ToArray()
            };
            await this.mediator.SendCommandAsync(saveIssuesCommand, cancellationToken);
            return new ImportIssuesResult(command.WorkspaceId, true);
        }
    }
}
