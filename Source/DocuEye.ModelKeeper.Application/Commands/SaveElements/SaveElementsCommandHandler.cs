
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DocuEye.ModelKeeper.Persistence;
using System.Linq;
using DocuEye.Infrastructure.Mediator.Commands;

namespace DocuEye.ModelKeeper.Application.Commands.SaveElements
{
    /// <summary>
    /// Handler for SaveElementsCommand
    /// </summary>
    public class SaveElementsCommandHandler : ICommandHandler<SaveElementsCommand>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB Context</param>
        public SaveElementsCommandHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles the command
        /// </summary>
        /// <param name="request">command request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task HandleAsync(SaveElementsCommand request, CancellationToken cancellationToken)
        {

            if (request.ElementsToAdd.Count() > 0)
            {
                await this.dbContext.Elements.InsertManyAsync(request.ElementsToAdd);
            }

            if (request.ElementsToChange.Count() > 0)
            {
                foreach (var element in request.ElementsToChange)
                {
                    await this.dbContext.Elements.ReplaceOneAsync(element);
                }
            }

            if (request.ElementsToDelete.Any())
            {
                foreach (var element in request.ElementsToDelete)
                {
                    await this.dbContext.Elements.DeleteOneAsync(o => o.Id == element.Id);
                }
            }
        }
    }
}
