using DocuEye.WorkspaceImporter.Application.Commands.ImportViews;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspaceImporter.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands
{
    public class BaseImportDataCommandHandler
    {
        protected readonly IWorkspaceImporterDBContext dbContext;

        public BaseImportDataCommandHandler(IWorkspaceImporterDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected async Task<WorkspaceImport> GetImport(string workspaceId, string importKey)
        {
            return await this.dbContext.WorkspaceImports
                .FindOne(o => o.WorkspaceId == workspaceId && o.Key == importKey);
        }

        protected ImportResult CheckImport(WorkspaceImport? import, string workspaceId, string importKey)
        {
            // If no import found then stop
            if (import == null)
            {
                return new ImportResult(
                        workspaceId,
                        false,
                        string.Format("No import found with key = '{0}'. Start import before continue.",
                            importKey));
            }
            // if import is already finished then stop
            if (import.EndTime != null)
            {
                return new ImportResult(
                        workspaceId,
                        false,
                        string.Format("Import with key = '{0}' is already finished.",
                            importKey));
            }

            return new ImportResult(workspaceId, true);
        }
    }
}
