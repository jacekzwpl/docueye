using DocuEye.ModelKeeper.Persistence;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using DocuEye.Infrastructure.Mediator.Commands;

namespace DocuEye.ModelKeeper.Application.Commands.SaveRelationships
{
    /// <summary>
    /// Handler for SaveRelationshipsCommand
    /// </summary>
    public class SaveRelationshipsCommandHandler : ICommandHandler<SaveRelationshipsCommand>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB Context</param>
        public SaveRelationshipsCommandHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles the command
        /// </summary>
        /// <param name="request">command request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task HandleAsync(SaveRelationshipsCommand request, CancellationToken cancellationToken)
        {

            if (request.RelationshipsToAdd.Count() > 0)
            {
                await this.dbContext.Relationships.InsertManyAsync(request.RelationshipsToAdd);
            }

            if (request.RelationshipsToChange.Count() > 0)
            {
                foreach (var relationship in request.RelationshipsToChange)
                {
                    await this.dbContext.Relationships.ReplaceOneAsync(relationship);
                }
            }

            if (request.RelationshipsToDelete.Any())
            {
                foreach (var relationship in request.RelationshipsToDelete)
                {
                    await this.dbContext.Relationships.DeleteOneAsync(o => o.Id == relationship.Id);
                }
            }
        }
    }
}
