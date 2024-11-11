using AutoMapper;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.StartImport
{
    public class StartImportCommandHandler : IRequestHandler<StartImportCommand, StartImportResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IWorkspaceImporterDBContext dbContext;

        public StartImportCommandHandler(IMediator mediator, IMapper mapper, IWorkspaceImporterDBContext dbContext)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<StartImportResult> Handle(StartImportCommand request, CancellationToken cancellationToken)
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
                AccessRules = this.mapper.Map<IEnumerable<WorkspaceAccessRule>>(request.AccessRules)
            };

            //Save Workspace
            await this.mediator.Send(new SaveWorkspaceCommand()
            {
                Workspace = workspace
            });

            return new StartImportResult(workspaceId, true);
        }
    }
}
