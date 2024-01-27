using DocuEye.ChangeTracker.Model;
using DocuEye.ChangeTracker.Persistence;
using DocuEye.Infrastructure.MongoDB;
using DocuEye.Infrastructure.Tests.FakeMongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ChangeTracker.Application.Tests
{
    public class FakeDbContext : IChangeTrackerDBContext
    {

        private List<ModelChange> modelChanges;

        public FakeDbContext() { 
        
            this.modelChanges = this.CreateModelChangesData();
        }

        public IGenericCollection<ModelChange> ModelChanges
        {
            get
            {
                return new FakeGenericCollection<ModelChange>(this.modelChanges);
            }
        }


        private List<ModelChange> CreateModelChangesData()
        {
            return new List<ModelChange>()
            {
                new ModelChange()
                {
                    ElementId = "elementtest1",
                    EventTime = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    Description = "Test description",
                    ImportId = "importtest1",
                    Type = ModelChangeType.ElementUpdated,
                    WorkspaceId = "workspacetest1"
                },
                new ModelChange()
                {
                    RelationshipId = "relationshiptest1",
                    EventTime = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    Description = "Test description",
                    ImportId = "importtest1",
                    Type = ModelChangeType.ElementUpdated,
                    WorkspaceId = "workspacetest1"
                }
            };
        }
    }
}
