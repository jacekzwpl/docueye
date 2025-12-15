using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.ModelKeeper.Model;

using System.Collections.Generic;
using System.Linq;

namespace DocuEye.ModelKeeper.Application.Commands.SaveRelationships
{
    /// <summary>
    /// Save relationships command
    /// </summary>
    public class SaveRelationshipsCommand : ICommand
    {
        /// <summary>
        /// Relationships that should be updated
        /// </summary>
        public IEnumerable<Relationship> RelationshipsToChange { get; set; } = Enumerable.Empty<Relationship>();
        /// <summary>
        /// Relationships that should be created in the model
        /// </summary>
        public IEnumerable<Relationship> RelationshipsToAdd { get; set; } = Enumerable.Empty<Relationship>();
        /// <summary>
        /// Relationships that should be removed from the model
        /// </summary>
        public IEnumerable<Relationship> RelationshipsToDelete { get; set; } = Enumerable.Empty<Relationship>();

    }
}
