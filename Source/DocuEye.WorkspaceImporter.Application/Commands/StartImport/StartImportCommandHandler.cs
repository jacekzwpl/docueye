using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Api.Model.Maps;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.StartImport
{
    public class StartImportCommandHandler : ICommandHandler<StartImportCommand, StartImportResult>
    {
        private readonly IMediator mediator;
        private readonly IWorkspaceImporterDBContext dbContext;

        public StartImportCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
        }

        public async Task<StartImportResult> HandleAsync(StartImportCommand request, CancellationToken cancellationToken)
        {

            var workspaceId = string.IsNullOrEmpty(request.WorkspaceId) ? Guid.NewGuid().ToString() : request.WorkspaceId;

            //Create import object
            var import = new WorkspaceImport()
            {
                Id = Guid.NewGuid().ToString(),
                Key = request.ImportKey,
                SourceLink = request.SourceLink,
                WorkspaceId = workspaceId,
                StartTime = DateTime.UtcNow,
            };

            // Get currently running import if any
            var runningImport = await this.dbContext.WorkspaceImports
                .FindOne(o => o.WorkspaceId == workspaceId && o.EndTime == null);

            if (runningImport != null)
            {
                //If currently running import has different key then stop
                if (runningImport.Key != import.Key)
                {
                    return new StartImportResult(
                        workspaceId,
                        false,
                        string.Format("Can not start import because another import with key = '{0}' is currently running.",
                            runningImport.Key));
                }
                //If key is the same means that for some reason import import did not finish and user wants to repeat action.
            }
            else
            {
                //if import with the same key already finished stop action
                var existingImport = await this.dbContext.WorkspaceImports
                .FindOne(o => o.WorkspaceId == workspaceId && o.Key == import.Key);
                if (existingImport != null)
                {
                    return new StartImportResult(
                        workspaceId,
                        false,
                        string.Format("Can not start import because another import with key = '{0}' is already completed.",
                            import.Key));
                }

                await this.dbContext.WorkspaceImports.InsertOneAsync(import);
            }

            var workspace = new Workspace()
            {
                Id = workspaceId,
                Name = request.WorkspaceName ?? workspaceId,
                Description = request.WorkspaceDescription,
                IsPrivate = request.Visibility.ToLower() == "private"
                    ? true : false,
                AccessRules = request.AccessRules.MapToWorkspaceAccessRules()
            };

            //Save Workspace
            await this.mediator.SendCommandAsync(new SaveWorkspaceCommand()
            {
                Workspace = workspace
            });

            return new StartImportResult(workspaceId, true);
        }
    }
}
